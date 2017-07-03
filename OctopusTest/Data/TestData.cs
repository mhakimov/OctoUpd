using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Excel;

namespace OctopusTest.Data
{
    class TestData
    {
        public static DataTable ExcelToDataTable(string fileName)
        {
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); 
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            DataTableCollection table = result.Tables;
            DataTable resultTable = table["Sheet1"];        
           return resultTable;
        }


        public static void PopulateInCollection(string fileName, List<DataCollection> dataCollection)
        {
            DataTable table = ExcelToDataTable(fileName);

            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dtTable = new DataCollection()
                    {
                        RowNumber = row,
                        ColName = table.Columns[col].ColumnName,
                        ColValue = table.Rows[row - 1][col].ToString()
                    };

                    dataCollection.Add(dtTable);
                }
            }
        }

      
        public static string ReadData(int rowNumber, string columnName, List<DataCollection> dataCollection)
        {         
                var data = (from colData in dataCollection
                               where colData.ColName == columnName && colData.RowNumber == rowNumber
                               select colData.ColValue).SingleOrDefault();

                return data;         
        }


        public static List<int> GetIndexValuesFromTable(List<DataCollection> table)
        {
            List<int> indexes = new List<int>();
            foreach (var dataCollection in table)
            {
                if (!indexes.Contains(dataCollection.RowNumber))
                    indexes.Add(dataCollection.RowNumber);

            }
            return indexes;
        }


        public static int GetRandomIndexFromTable(List<DataCollection> table)
        {
            return Utilities.SelectRandomElement(GetIndexValuesFromTable(table));
        }


        public static List<string> GetEmployeeNamesFromIndexes(List<int> indexes, List<DataCollection> table, string columnName)
        {
            List<string> employeesNamesInDb = new List<string>();
            foreach (var index in indexes)
            {
                employeesNamesInDb.Add(table.
                    First(e => e.ColName == columnName && e.RowNumber == index).ColValue);
            }
            return employeesNamesInDb;
        }


        public static string GetStringValueThatDoesNotExistInDb(List<DataCollection> dataCollection, string column)
        {
            var i = 0;
            var incorrectName = Utilities.GetRandomString(15);
            while (dataCollection.Where(c => c.ColName == column).Any(c => c.ColValue == incorrectName) && i < 10)
            {
                incorrectName = Utilities.GetRandomString(15);
                i++;
            }
            return incorrectName;
        }
    }
}

