using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class ArbolPrueba<T> where T : IComparable
    {
        public NodoVector<T> Raiz;
        int Degree;
        bool RaizEntrar = true;
        bool RaizSubir = false; 

        public ArbolPrueba(int Grado)
        {
            Degree = Grado;
            NodoVector<T> NodoV = new NodoVector<T>(Grado);// Es un espacio mas, cuando se llene por conpleto se tiene que balancear
            NodoV.Vector = new NodoArbolB<T>[Grado];
            Raiz = NodoV;
        }

        public void Insert(T NewNodo, Delegate Comparacion)
        {
            NodoArbolB<T> NuevoMe = new NodoArbolB<T>();
            NuevoMe.Data = NewNodo;
            if (Raiz.Vector[0] == null)
            {
                Raiz.Vector[0] = NuevoMe;
            }
            else
            {
                Insert(NuevoMe, Raiz, Comparacion);
            }
        }
        
        public void EliminarArbol() 
        {
            Raiz = null;
        }
    }
}