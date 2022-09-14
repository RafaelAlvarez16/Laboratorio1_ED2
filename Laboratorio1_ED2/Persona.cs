using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class Persona : IComparable
    {
        public string Nombre { get; set; }
        public string DPI { get; set; }
        public string Fecha_Nacimiento { get; set; }
        public string Direccion { get; set; }
        public List<string> Companias { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
        public int CompareToNombre(string nom1, string nom2)
        {
            return nom1.CompareTo(nom2);
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
    }

}
