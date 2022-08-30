using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class EstructuraArbol
    {
        public NodoVector Raiz;
        int Degree;
        bool RaizEntrar = true;
        bool RaizSubir = true;

        public EstructuraArbol(int Grado)
        {
            Degree = 5;
            NodoVector NodoV = new NodoVector();// Es un espacio mas, cuando se llene por conpleto se tiene que balancear
            NodoV.Vector = new NodoArbolB[5];
            Raiz = NodoV;
        }
        public void Insert(string Nombre, string Dpi, Persona nuevo, Delegate comparacion) 
        {

            NodoArbolB NuevoMe = new NodoArbolB();
            NuevoMe.Nombre = Nombre;
            NuevoMe.DPI = Dpi;
            NuevoMe.Nuevo = nuevo;

            if (Raiz.Vector[0] == null)
            {
                Raiz.Vector[0] = NuevoMe;
            }
            else
            {
                Insert(NuevoMe, Raiz, comparacion);
            }
        }
        public void Insert(NodoArbolB NewNodo, NodoVector VectorPadre, Delegate Comparacion)
        {
            if (VectorPadre.Vector[0].Izquierda==null)
            {
                bool ciclo = true;
                for (int i = 0; i <= Degree-1 && ciclo==true ; i++)
                {
                    if (VectorPadre.Vector[i] == null)
                    {
                        if (RaizSubir)
                        {

                        }
                        VectorPadre.Vector[i] = NewNodo;
                        ciclo = false;
                    }
                }
            }
        }
    }
}
