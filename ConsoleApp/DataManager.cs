using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class DataManager
    {
        enum ObjectElements
        {
            TYPE,
            NAME,
            SCHEMA,
            PARENTNAME,
            PARENTTYPE,
            DATATYPE,
            ISNULLABLE
        }

        private IEnumerable<ImportedObject> mImportedObjects;

        public DataManager()
        {
            mImportedObjects = new List<ImportedObject>() { };
        }


        // Clear and correct imported data
        // Input:   data
        private String PrepareElement(String element)
        {
            return element.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }

        // Prepare raw lines to modificable data
        // Input:   list of file lines
        private void PrepareData(List<string> importedLines)
        {
            uint lineNum = 0; // for error logs

            foreach (var line in importedLines)
            {
                lineNum++;
                var values = line.Split(';');
                if (line.Any()) // ignore empty lines
                {
                    var importedObject = new ImportedObject();
                    
                    try
                    {

                        importedObject.Type         = PrepareElement(values[(int)ObjectElements.TYPE]).ToUpper();
                        importedObject.Name         = PrepareElement(values[(int)ObjectElements.NAME]);
                        importedObject.Schema       = PrepareElement(values[(int)ObjectElements.SCHEMA]);
                        importedObject.ParentName   = PrepareElement(values[(int)ObjectElements.PARENTNAME]);
                        importedObject.ParentType   = PrepareElement(values[(int)ObjectElements.PARENTTYPE]).ToUpper();
                        importedObject.DataType     = PrepareElement(values[(int)ObjectElements.DATATYPE]);
                        importedObject.IsNullable   = PrepareElement(values[(int)ObjectElements.ISNULLABLE]);
                    }
                    catch (Exception ex) 
                    {
                        DataPrinter.LogException(ex.Message, $"\ton file line {lineNum}: {line}\n\tMissing any entry?");
                    }

                    ((List<ImportedObject>)mImportedObjects).Add(importedObject);
                }
            }

            // assign number of children
            foreach (var element in mImportedObjects)
            {
                foreach (var impObj in mImportedObjects)
                {
                    if ((impObj.ParentType == element.Type) &&
                        (impObj.ParentName == element.Name))
                    {
                        element.NumberOfChildren++;
                    }
                }
            }
        }

        // Load data and prepare to show
        // Input:   path to file
        public void LoadAndPrepare(string fileToImport)
        {
            List<string> fileData = DataReader.ImportData(fileToImport);

            if (fileData.Any())
            {
                PrepareData(fileData);
            }
        }

        // Print existing data
        public void Print()
        {
            if (mImportedObjects.Any())
            {
                DataPrinter.Print((List<ImportedObject>)mImportedObjects);
            }
            else
            {
                Console.WriteLine("No data to show.");
            }
        }
    }
}
