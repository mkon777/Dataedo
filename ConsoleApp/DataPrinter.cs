using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class DataPrinter
    {
        // Printing data
        // Input:   list of data
        static public void Print(List<ImportedObject> data) 
        {
            foreach (var database in data)
            {
                if (database.Type == "DATABASE")
                {
                    Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    // print all database's tables
                    foreach (var table in data)
                    {   
                        if ((table.ParentType.ToUpper() == database.Type) && 
                            (table.ParentName == database.Name))
                        {
                            Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                            // print all table's columns
                            foreach (var column in data)
                            {
                                if ((column.ParentType.ToUpper() == table.Type) && 
                                    (column.ParentName == table.Name))
                                {
                                    Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                                }
                            }
                        }
                    }
                }
            }
        }

        // Printing exception message
        // Input:   exception message
        //          (optional) additional info
        static public void LogException(string message, string moreInfo = "")
        {
            Console.WriteLine(message);
            if (moreInfo.Any())
            {
                Console.WriteLine(moreInfo);
            }
        }

    }
}
