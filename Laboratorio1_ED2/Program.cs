using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Laboratorio1_ED2
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader(File.OpenRead(@"C:\Users\randr\OneDrive\Documents\URL\2022\Segundo Ciclo\Estructura ll\input.csv"));
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                var newvalues = values[1].ToString().Split('"');

            }
            Console.WriteLine("Hello World!");
        }
    }
}
