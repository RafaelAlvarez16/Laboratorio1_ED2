using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class Persona : IComparable
    {
        public string Nombre { get; set; }
        public string DP1 { get; set; }
        public string Fehca { get; set; }
        public string Direccion { get; set; }
    }
    public int CompareToString(string obj1, string obj2)
    {
        int Comparar = obj1.CompareTo(obj2);
        if (Comparar == 0)
            return 0;
        else if (Comparar < 0)
            return -1;
        else
            return 1;
    }
    public int CompareToObjeto(object obj1, string obj2)
    {
        int Comparar = Convert.ToString(obj1).CompareTo(obj2);
        if (Comparar == 0)
            return 0;
        else if (Comparar < 0)
            return -1;
        else
            return 1;
    }
}
