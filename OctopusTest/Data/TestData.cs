using Excel;
using OctopusTest.Data;
using OctopusTest.Methods;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusTest
{
    class TestData
    {

        public static DataTable ExcelToDataTable(string fileName)
        {
            //open file and returns as Stream
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
            //Set the First Row as Column Name
            excelReader.IsFirstRowAsColumnNames = true;
            //Return as DataSet
            DataSet result = excelReader.AsDataSet();
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["Sheet1"];

            //return
            return resultTable;
        }


        //static List<DataCollection> dataCollection = new List<DataCollection>();
      //  static List<DataCollection> employeesDataCollection = new List<DataCollection>();

        public static void PopulateInCollection(string fileName, List<DataCollection> dataCollection)
        {
            DataTable table = ExcelToDataTable(fileName);

            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dtTable = new DataCollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    dataCollection.Add(dtTable);
                }
            }
        }

        public static string GetStringValueThatDoesNotExistInDb(List<DataCollection> dataCollection, string column)
        {
            var i = 0;
            var incorrectName = GeneralMethods.GetRandomString(15);
            while (dataCollection.Where(c => c.colName == column).Any(c => c.colValue == incorrectName) && i < 10)
            {
                incorrectName = GeneralMethods.GetRandomString(15);
                i++;
            }
            return incorrectName;
        }


        public static string ReadData(int rowNumber, string columnName, List<DataCollection> dataCollection)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCollection
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                // string data1 = dataCol.Where(c=> c.colName == columnName && c.rowNumber == rowNumber).SingleOrDefault().colValue
               // var data2 = dataCol.FirstOrDefault(c => c.colName == columnName && c.rowNumber == rowNumber).colValue;


                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }

 
}

