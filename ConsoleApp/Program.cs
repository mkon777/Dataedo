namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new DataManager();
            data.LoadAndPrepare("data.csv");
            data.Print();

            Console.ReadLine();
        }
    }
}
