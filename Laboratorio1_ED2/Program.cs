using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Laboratorio1_ED2
{
    public class Program<T> where T : IComparable
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader(File.OpenRead(@"C:\Users\randr\OneDrive\Documents\URL\2022\Segundo Ciclo\Estructura ll\input.csv"));
            ArbolPrueba<T> MyStruct = new ArbolPrueba<T>(5);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                var json1 = values[1].Split('"');

                if (values[0]=="INSERT")
                {
                    var PersonaD = new Persona
                    {
                        Nombre = json1[3],
                        DPI = json1[7],
                        Fecha_Nacimiento = json1[11],
                        Direccion = json1[15]
                    };
                    MyStruct.Insert(PersonaD);
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
