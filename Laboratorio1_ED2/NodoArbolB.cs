using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class NodoArbolB
    {
        public string Nombre { get; set; }
        public string DPI { get; set; }
        public Persona Nuevo { get; set; }

        public NodoVector Derecha { get; set; }
        public NodoVector Izquierda { get; set; }
    }
}
