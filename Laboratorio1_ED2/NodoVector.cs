using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    
    public class NodoVector
    {
        public Persona[] Vector { get; set; }

        public NodoVector()
        {
            Vector = new Persona[11];
        }
    }
}
