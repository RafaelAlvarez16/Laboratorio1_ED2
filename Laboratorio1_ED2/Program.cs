using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Laboratorio1_ED2
{
    public class Program
    {
        delegate int DelegadosN(string Nombre1, string Nombre2);
        public static void Main(string[] args)
        {
            EstructuraHash estructuraHash = new EstructuraHash();
            List<Persona> Nueva2 = new List<Persona>();
            Dictionary<string, string> Diccionario = new Dictionary<string, string>();
            NodoVector[] TablaHash = new NodoVector[300];
            for (int i = 0; i < 300; i++)
            {
                TablaHash[i] = new NodoVector();
            }

            var reader = new StreamReader(File.OpenRead(@"C:\Users\randr\Downloads\input.csv"));
            
            bool verificar = true;
            while (!reader.EndOfStream && verificar)
            {
                List<string> Nueva1 = new List<string>();

                var line = reader.ReadLine();
                var values = line.Split(';');
                var json1 = values[1].Split('"');

                for (int i = 19; i < json1.Length; i++)
                {
                    Nueva1.Add(json1[i]);
                    i++;
                }

                if (values[0] == "INSERT")
                {
                    var PersonaD = new Persona
                    {
                        //COMPANIES 17
                        Nombre = json1[3],
                        DPI = json1[7],
                        Fecha_Nacimiento = json1[11],
                        Direccion = json1[15],
                        Companias = Nueva1
                    };
                    Nueva2.Add(PersonaD);
                }
                else if (values[0]=="PATCH")
                {
                    var PersonaN = new Persona
                    {
                        //COMPANIES 17
                        Nombre = json1[3],
                        DPI = json1[7],
                        Fecha_Nacimiento = json1[11],
                        Direccion = json1[15],
                        Companias = Nueva1
                    };
                    for (int i = 0; i < Nueva2.Count; i++)
                    {
                        if (PersonaN.DPI == Nueva2[i].DPI)
                        {
                            Nueva2.RemoveAt(i);
                            i = 400;
                        }
                    }
                    Nueva2.Add(PersonaN);
                }
                else
                {
                    var PersonaN = new Persona
                    {
                        //COMPANIES 17
                        Nombre = json1[3],
                        DPI = json1[7],
                        Fecha_Nacimiento = json1[11],
                        Direccion = json1[15],
                        Companias = Nueva1
                    };

                    for (int i = 0; i < Nueva2.Count; i++)
                    {
                        if (PersonaN.DPI==Nueva2[i].DPI)
                        {
                            Nueva2.RemoveAt(i);
                            i = 400;
                        }
                    }
                }

            }
            bool openArbol = true;
            bool Openconpanimes = true;
            while (openArbol)
            {
                Console.WriteLine();
                Console.WriteLine("Seleccione la opcion que desea realizar");
                Console.WriteLine("1) Buscar por nombre");
                Console.WriteLine("2) Salir");
                int op= Convert.ToInt32(Console.ReadLine());
                if (op==1)
                {
                    Console.WriteLine("Ingrese Nombre");
                    string opcion = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Ingrese DPI");
                    string opcion2 = Convert.ToString(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("Las datos encontrados son:");
                    List<string> Dicciona = new List<string>();
                    List<string> EncodeDicciona = new List<string>();
                    int PosiDPI = 0;
                    int globa = 1;
                    for (int i = 0; i < Nueva2.Count; i++)
                    {
                        if (opcion == Nueva2[i].Nombre && opcion2==Nueva2[i].DPI)
                        {
                            PosiDPI = i;
                            //SI ENCONTRO EL NOMBRE QUE ESTA BUSCANDO SE CODIFICA ANTES DE IMPRIMIR
                            Console.WriteLine("name:" + Nueva2[i].Nombre + " DPI:" + Nueva2[i].DPI + " Address:" + Nueva2[i].Direccion + " Fecha:" + Nueva2[i].Fecha_Nacimiento);
                            int conta = 1;
                            bool badenra = true;
                            while (badenra)
                            {
                                bool siexiste = File.Exists(@"C:\Users\randr\OneDrive\Escritorio\Cartes de Recomendacion\inputs\inputs\REC-" + Nueva2[i].DPI + "-"+conta+".txt");
                                if (siexiste)
                                {
                                    Console.WriteLine("REC-" + Nueva2[i].DPI + "-" + conta + ".txt");
                                    string text = System.IO.File.ReadAllText(@"C:\Users\randr\OneDrive\Escritorio\Cartes de Recomendacion\inputs\inputs\REC-" + Nueva2[i].DPI + "-"+conta+".txt");
                                    string cartacodificada=estructuraHash.LZ78_Encode(text);
                                    System.IO.File.WriteAllText(@"C:\Users\randr\OneDrive\Escritorio\Cartes de Recomendacion\output\"+"compressed-REC-"+Nueva2[i].DPI+conta+".txt", cartacodificada);
                                    Console.WriteLine("compressed-REC-" + Nueva2[i].DPI + conta + ".txt");
                                    conta++;
                                }
                                else
                                {
                                    badenra = false;
                                }
                                Console.WriteLine();
                            }
                            //Console.WriteLine("Companias:");
                            //for (int j = 0; j < Nueva2[i].Companias.Count; j++)
                            //{
                            //    string nuevoE = estructuraHash.LZ78_Encode(opcion2, Nueva2[i].Companias[j]);
                            //    Console.WriteLine("     " + Nueva2[i].Companias[j] + "   DPI:" + nuevoE);
                            //    Dicciona.Add(Nueva2[i].Companias[j]);
                            //    EncodeDicciona.Add(nuevoE);
                            //}
                            //i = Nueva2.Count;
                        }
                    }
                    Openconpanimes = true;
                    while (Openconpanimes)
                    {
                        Console.WriteLine("Para salir seleccione 1");
                        Console.WriteLine("Ingrese la carta que desea descomprimir");
                        string opcion3 = Convert.ToString(Console.ReadLine());
                        if (opcion3 !=  "1")
                        {
                            bool siexiste = File.Exists(@"C:\Users\randr\OneDrive\Escritorio\Cartes de Recomendacion\output\"+opcion3);
                            if (siexiste)
                            {
                                string text = System.IO.File.ReadAllText(@"C:\Users\randr\OneDrive\Escritorio\Cartes de Recomendacion\output\"+opcion3);
                                string cartacodificada = estructuraHash.LZ78_Dencode(text);
                                System.IO.File.WriteAllText(@"C:\Users\randr\OneDrive\Escritorio\Cartes de Recomendacion\newInput\" + "desscompressed-REC-" + Nueva2[PosiDPI].DPI + globa+".txt", cartacodificada);
                                globa++;
                            }
                        }
                        else
                        {
                            Openconpanimes = false;
                        }
                    }
                }
                else if (op==2)
                {
                    openArbol = false;
                }
                else
                {
                    Console.WriteLine("No existe la opcion");
                }

            }


        }
      
    }
}
