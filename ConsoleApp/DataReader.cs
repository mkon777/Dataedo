namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataReader
    {

        // Loading data from file
        // Input:   file path
        // Output:  List of text lines from file
        static public List<string> ImportData(string fileToImport)
        {
            try
            {
                var streamReader = new StreamReader(fileToImport);
                var importedLines = new List<string>();

                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    importedLines.Add(line);
                }

                return importedLines;
            }
            catch (Exception ex) 
            {
                DataPrinter.LogException(ex.Message);
            }

            return Enumerable.Empty<string>().ToList();

        }
    }

    class ImportedObject : ImportedObjectBaseClass
    {
        public string Name
        {
            get;
            set;
        }
        public string Schema;

        public string ParentName;
        public string ParentType
        {
            get; set;
        }

        public string DataType { get; set; }
        public string IsNullable { get; set; }

        public double NumberOfChildren;
    }

    class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
