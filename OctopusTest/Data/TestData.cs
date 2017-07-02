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
    }
}

