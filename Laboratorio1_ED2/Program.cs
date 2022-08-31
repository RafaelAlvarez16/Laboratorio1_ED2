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
            List<Persona> Nueva = new List<Persona>();
            List<Persona> Nueva2 = new List<Persona>();
            EstructuraArbol MyStructur = new EstructuraArbol(5);
            var reader = new StreamReader(File.OpenRead(@"C:\Users\randr\OneDrive\Documents\URL\2022\Segundo Ciclo\Estructura ll\input.csv"));
            Persona CallDatosNombre = new Persona();
            DelegadosN InvocarNombre = new DelegadosN(CallDatosNombre.CompareToNombre);
            int cont = 0;
            bool verificar = true;
            while (!reader.EndOfStream && verificar)
            {
                cont++;
                if (cont==19)
                {
                    verificar = false;
                }
                var line = reader.ReadLine();
                var values = line.Split(';');
                var json1 = values[1].Split('"');

                if (values[0] == "\"INSERT")
                {
                    var PersonaD = new Persona
                    {
                        Nombre = json1[6],
                        DPI = json1[12],
                        Fecha_Nacimiento = json1[18],
                        Direccion = json1[24]
                        //Nombre = json1[3],
                        //DPI = json1[7],
                        //Fecha_Nacimiento = json1[11],
                        //Direccion = json1[15]
                    };
                    Nueva2.Add(PersonaD);
                    MyStructur.Insert(PersonaD.Nombre, PersonaD.DPI, PersonaD, InvocarNombre);
                }
                else if (values[0]=="\"PATCH")
                {
                    var PersonaDos = new Persona
                    {
                        Nombre = json1[6],
                        DPI = json1[12],
                        Fecha_Nacimiento = json1[18],
                        Direccion = json1[24]
                        //Nombre = json1[3],
                        //DPI = json1[7],
                        //Fecha_Nacimiento = json1[11],
                        //Direccion = json1[15]
                    };
                    for (int i = 0; i < Nueva2.Count; i++)
                    {
                        if (PersonaDos.Nombre==Nueva2[i].Nombre && PersonaDos.DPI == Nueva2[i].DPI)
                        {
                            Nueva2[i] = PersonaDos;
                        }
                    }
                }
                else
                {
                    var Personat = new Persona
                    {
                        Nombre = json1[6],
                        DPI = json1[12],
                        Fecha_Nacimiento = json1[18],
                        Direccion = json1[24]
                        //Nombre = json1[3],
                        //DPI = json1[7],
                        //Fecha_Nacimiento = json1[11],
                        //Direccion = json1[15]
                    };
                    for (int i = 0; i < Nueva2.Count; i++)
                    {
                        if (Personat.Nombre == Nueva2[i].Nombre && Personat.DPI == Nueva2[i].DPI)
                        {
                            Nueva2.RemoveAt(i);
                        }
                    }
                }

            }
            bool openArbol = true;
            while (openArbol)
            {
                Console.WriteLine();
                Console.WriteLine("Seleccione la opcion que desea realizar");
                Console.WriteLine("1) Buscar por nombre");
                Console.WriteLine("2) Salir");
                int op= Convert.ToInt32(Console.ReadLine());
                if (op==1)
                {
                    Console.WriteLine("Ingrese nombre");
                    string opcion = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Las datos encontrados son:");
                    //MyStructur.limpiar();
                    //MyStructur.Buscar(opcion, MyStructur.Raiz, InvocarNombre);
                    //Nueva = MyStructur.retornar();
                    //for (int i = 0; i < Nueva.Count; i++)
                    //{
                    //    Console.WriteLine("name:" + Nueva[i].Nombre + " DPI:" + Nueva[i].DPI + " Address:" + Nueva[i].Direccion + " Fecha:" + Nueva[i].Fecha_Nacimiento);
                    //}

                    for (int i = 0; i < Nueva2.Count; i++)
                    {
                        if (opcion == Nueva2[i].Nombre)
                        {
                            Console.WriteLine("name:" + Nueva2[i].Nombre + " DPI:" + Nueva2[i].DPI + " Address:" + Nueva2[i].Direccion + " Fecha:" + Nueva2[i].Fecha_Nacimiento);
                        }
                    }
                }
                else if (op==5)
                {

                }
                else if (op==3)
                {

                }
                else if (op==4)
                {

                }
                else if (op==2)
                {
                    openArbol = false;
                }
                else
                {
                    Console.WriteLine("No existe la opcion");
                }
                {

                }

            }


        }
      
    }
}
