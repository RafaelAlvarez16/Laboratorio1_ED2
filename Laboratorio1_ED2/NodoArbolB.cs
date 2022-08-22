using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class NodoArbolB<T> where T : IComparable
    {
        public T Data { get; set; }
        public NodoVector<T> Derecha { get; set; }
        public NodoVector<T> Izquierda { get; set; }

        public NodoArbolB()
        {
            this.Data = default;
            this.Derecha = null;
            this.Izquierda = null; 
        }
    }
}
