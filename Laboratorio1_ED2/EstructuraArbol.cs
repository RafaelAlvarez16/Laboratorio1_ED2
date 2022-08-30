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
                if (Nombre=="hassie")
                {
                    int a;
                    a = 9;
                }
                Insert(NuevoMe, Raiz, comparacion);
            }
        }
        public void Insert(NodoArbolB NewNodo, NodoVector VectorPadre, Delegate Comparacion)
        {
            if (VectorPadre.Vector[0].Izquierda == null)
            {
                bool ciclo = true;
                for (int i = 0; i <= Degree - 1 && ciclo == true; i++)
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
                NodoArbolB Auxiliar = new NodoArbolB();
                VectorPadre.Vector = ShellSort(VectorPadre.Vector, Comparacion);
                if (FullVector(VectorPadre.Vector))
                {
                    if (RaizEntrar)
                    {
                        Auxiliar = UploadNode(VectorPadre.Vector);
                        Raiz.Vector = VaciarVector(Raiz.Vector);
                        Raiz.Vector[0] = Auxiliar;
                        Raiz.Vector[0].Izquierda.Padre = Raiz;
                        Raiz.Vector[0].Derecha.Padre = Raiz;
                        Raiz.Padre = null;
                        RaizEntrar = false;
                    }
                    else
                    {
                        if (FullVector(Raiz.Vector))
                        {
                            NodoVector Nuevo = new NodoVector();
                            Auxiliar = UploadNode(Raiz.Vector);
                            Nuevo.Vector[0] = Auxiliar;
                            Raiz = Nuevo;
                            Insert(NewNodo, Raiz, Comparacion);
                        }
                        else
                        {
                            Auxiliar = UploadNode(VectorPadre.Vector);
                            InsertNotRoot(Auxiliar, VectorPadre.Padre, Comparacion);
                        }
                    }
                }
            }
            else
            {
                int comp = 0;
                bool VerificacionEntrada = true;
                for (int i = 0; i <= Degree-2 && VerificacionEntrada; i++)
                {
                    comp = Convert.ToInt32(Comparacion.DynamicInvoke(NewNodo.Nombre, VectorPadre.Vector[i].Nombre));
                    if (comp < 0)
                    {
                        comp = -1;
                        VerificacionEntrada = false;
                        Insert(NewNodo, VectorPadre.Vector[i].Izquierda, Comparacion);
                        VectorPadre.Vector[i].Izquierda.Padre = VectorPadre;
                    }
                    else
                    {
                        if (VectorPadre.Vector[i + 1] != null)
                        {
                            VerificacionEntrada = true;
                        }
                        else
                        {
                            VerificacionEntrada = false;
                            Insert(NewNodo, VectorPadre.Vector[i].Derecha, Comparacion);
                            if (VectorPadre.Vector[i] != null)
                            {
                                VectorPadre.Vector[i].Derecha.Padre = VectorPadre;
                            }
                        }
                    }
                }
            }
        }

        public void InsertNotRoot(NodoArbolB NodeToInsert, NodoVector Padre, Delegate Comparacion)
        {
            bool Ciclo = true;
            int Rangos = 0;
            NodoArbolB Aux = new NodoArbolB();
            for (int i = 0; i <= Degree - 1 && Ciclo == true; i++)
            {
                if (Padre.Vector[i] == null)
                {
                    Padre.Vector[i] = NodeToInsert;
                    Rangos = i;
                    Ciclo = false;
                }
            }
            int comp = Convert.ToInt32(Comparacion.DynamicInvoke(NodeToInsert.Nombre, Padre.Vector[0].Nombre));
            Padre.Vector = ShellSort(Padre.Vector, Comparacion);
            if (comp < 0)
            {
                Padre.Vector[Rangos].Izquierda = Padre.Vector[Rangos - 1].Derecha;
            }
            else if (Rangos == Degree - 1)
            {
                if (FullVector(Raiz.Vector))
                {
                    Aux = UploadNode(Padre.Vector);
                    Raiz.Vector = VaciarVector(Raiz.Vector);
                    Raiz.Vector[0] = Aux;
                    Raiz.Vector[0].Izquierda.Padre = Raiz;
                    Raiz.Vector[0].Derecha.Padre = Raiz;
                    Raiz.Padre = null;
                    RaizEntrar = false;
                }
                else
                {
                    Aux = UploadNode(Padre.Vector);
                    InsertNotRoot(Aux, Padre.Padre, Comparacion);
                }
            }
            else
            {
                if (Padre.Vector[Rangos + 1] != null)
                    Padre.Vector[Rangos + 1].Izquierda = Padre.Vector[Rangos].Derecha;

                if (Padre.Vector[Rangos - 1] != null)
                    Padre.Vector[Rangos - 1].Derecha = Padre.Vector[Rangos].Izquierda;
            }
        }
        public NodoArbolB[] VaciarVector(NodoArbolB[] NodoAVaciar)
        {
            for (int i = 0; i <= Degree - 1; i++)
            {
                NodoAVaciar[i] = null;
            }
            return NodoAVaciar;
        }
        public NodoArbolB[] ShellSort(NodoArbolB[] Ordenar, Delegate Condicion)
        {
            for (int i = 0; i < Degree - 1; i++)
            {
                for (int j = 0; j < (Degree - 1) - i; j++)
                {
                    if (Ordenar[j] != null && Ordenar[j + 1] != null)
                    {
                        int Comparar = Convert.ToInt32(Condicion.DynamicInvoke(Ordenar[j].Nombre, Ordenar[j + 1].Nombre));
                        if (Comparar > 0)
                        {
                            NodoArbolB aux;
                            aux = Ordenar[j];
                            Ordenar[j] = Ordenar[j + 1];
                            Ordenar[j + 1] = aux;
                        }
                    }
                }
            }
            return Ordenar;
        }
        public NodoArbolB UploadNode(NodoArbolB[] UploadVector)
        {
            NodoArbolB Aux = new NodoArbolB();
            NodoVector Left = new NodoVector();
            NodoVector Right = new NodoVector();
            int div = 0;
            if (Impar())
            {
                div = (Degree / 2) - 1;
                Aux = UploadVector[div];
            }
            else
            {
                div = (Degree / 2);
                Aux = UploadVector[div];
            }
            for (int i = 0; i < div; i++)
            {
                Left.Vector[i] = UploadVector[i];
            }
            int Begin = 0;
            for (int i = Degree - 1; i > div; i--)
            {
                Right.Vector[Begin] = UploadVector[i];
                Begin++;
            }
            Aux = UploadVector[div];
            Aux.Izquierda = Left;
            Aux.Derecha = Right;

            return Aux;
        }
        public bool Impar()
        {
            int Comprobacion = Degree % 2;
            if (Comprobacion == 0)
            {
                return true;// es par
            }
            else
            {
                return false;// es impar
            }
        }

        public bool FullVector(NodoArbolB[] Verificar)
        {
            bool Vacio = true;
            for (int i = 0; i <= Degree - 1; i++)
            {
                if (Verificar[i] == null)
                {
                    Vacio = false;
                }
            }
            return Vacio;
        }
    }
}
