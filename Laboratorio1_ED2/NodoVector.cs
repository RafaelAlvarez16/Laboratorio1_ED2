using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class NodoVector <T> where T : IComparable
    {
        public NodoArbolB<T>[] Vector { get; set; }
        public NodoVector<T> Padre { get; set; }
        public int Altura { get; set; }

        public NodoVector(int MaxLenght)
        {
            Vector = new NodoArbolB<T>[MaxLenght];
        }
    }
}
