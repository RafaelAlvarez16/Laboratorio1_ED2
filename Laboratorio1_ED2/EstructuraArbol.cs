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
                    if (comp <= 0)
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
            int Modificar = 0;
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
            Ciclo = true;
            int comp = Convert.ToInt32(Comparacion.DynamicInvoke(NodeToInsert.Nombre, Padre.Vector[0].Nombre));
            Padre.Vector = ShellSort(Padre.Vector, Comparacion);
            for (int i = 0; i <= Degree - 1; i++)
            {
                if (Padre.Vector[i] == NodeToInsert)
                {
                    Modificar = i + 1;
                }
            }
            if (comp < 0)
            {
                if (Modificar!=0)
                {
                    Padre.Vector[Modificar].Izquierda = Padre.Vector[Modificar - 1].Derecha;
                }
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

        public void Delete(string nombre, string dpi, NodoVector Capsule, Delegate Comparacion)
        {
            bool verificar = true;
            for (int i = 0; i < Capsule.Vector.Length - 1 && verificar; i++)
            {
                int comp1 = Convert.ToInt32(Comparacion.DynamicInvoke(nombre, Capsule.Vector[i].Nombre));
                int comp2 = Convert.ToInt32(Comparacion.DynamicInvoke(dpi, Capsule.Vector[i].DPI));
                if (comp1 == 0 && comp2 == 0)
                {
                    verificar = false;
                    DeleteForm(i, Capsule, Comparacion);
                }
                else if (comp1 < 0)
                {
                    if (Capsule.Vector[i].Izquierda != null)
                    {
                        verificar = false;
                        Delete(nombre, dpi, Capsule.Vector[i].Izquierda, Comparacion);
                    }
                }
                else
                {
                    if (Capsule.Vector[i + 1] == null)
                    {
                        verificar = false;
                        Delete(nombre, dpi, Capsule.Vector[i].Derecha, Comparacion);
                    }
                    else
                    {
                    }
                }
            }
        }
        public void DeleteForm(int num, NodoVector Vector, Delegate Comparacion)
        {
            if (Vector.Vector[num].Derecha == null && Vector.Vector[num].Izquierda == null) //verificar si no tiene hijos
            {
                if (Vector.Padre==null) //verificar si no tiene padre
                {
                    if (Vector.Vector[num + 1] == null)
                    {
                        Vector.Vector[num] = null;
                    }
                    else
                    {
                        Vector.Vector[num] = null;
                        OrdenarEspacios(Vector, num);
                    }
                }
                else // es un vector sin hijos pero con un padre hoja
                {
                    int Minposible = 0;
                    int PoseeHojas = 0;
                    if (Impar())
                    {
                        Minposible = (Degree / 2) - 1;
                    }
                    else
                    {
                        Minposible = (Degree / 2);
                    }
                    for (int i = 0; i < Degree-2; i++)
                    {
                        if (Vector.Vector[i] != null)
                        {
                            PoseeHojas++;
                        }
                    }
                    if (PoseeHojas==Minposible) //posee lo mismo que las hojas
                    {

                    }
                    else if (Minposible < PoseeHojas)//tiene mas del minimo en las hojas
                    {
                        Vector.Vector[num] = null;
                        OrdenarEspacios(Vector, num);
                    }
                }
            }
            else //si tiene hijos 
            {
                if (Vector.Padre==null)//ver si tien padre
                {
                    if (Vector.Vector[1]==null)
                    {
                        int hijosizqmas = 0;
                        int hijosizqmen = 0;
                        NodoArbolB Auxiliar = new NodoArbolB();
                        for (int i = 0; i < Vector.Padre.Vector.Length; i++)
                        {
                            if (Vector.Vector[0].Izquierda.Vector[i] != null)
                            {
                                hijosizqmen++;
                            }
                            if (Vector.Vector[0].Derecha.Vector[i] != null)
                            {
                                hijosizqmas++;
                            }
                        }
                        if (hijosizqmen > hijosizqmas)
                        {
                            Vector.Vector[0] = Vector.Vector[0].Izquierda.Vector[hijosizqmen - 1];
                            Vector.Vector[0].Izquierda.Vector[hijosizqmen - 1] = null;
                        }
                        else if (hijosizqmas>hijosizqmen)
                        {
                            Vector.Vector[0] = Vector.Vector[0].Derecha.Vector[0];
                            Vector.Vector[0].Derecha.Vector[0] = null;
                            OrdenarEspacios(Vector.Vector[0].Derecha, 0);
                        }
                        else
                        {
                            for (int i = 0; i < hijosizqmen; i++)
                            {
                                Vector.Vector[i] = Vector.Vector[i].Izquierda.Vector[i];
                            }
                            for (int i = hijosizqmas - 1; i < Vector.Vector.Length; i++)
                            {
                                if (hijosizqmas > 0)
                                {
                                    int a = 0;
                                    Vector.Vector[i] = Vector.Vector[i].Derecha.Vector[a];
                                }
                            }
                        }
                    }
                }
            }
        }
        public void OrdenarEspacios(NodoVector Vector, int num) 
        {
            bool verificacion = true;
            for (int i = num; i < Vector.Vector.Length - 2 && verificacion; i++)
            {
                NodoArbolB Aux = null;
                if (Vector.Vector[i + 1] != null)
                {
                    Aux = Vector.Vector[i + 1];
                    Vector.Vector[i] = Aux;
                    Vector.Vector[i + 1] = null;
                }
            }
        }
        List<Persona> NuevaLista = new List<Persona>();
        public void limpiar() 
        {
            NuevaLista.Clear();
        }
        public List<Persona> retornar() 
        {
            return NuevaLista;
        }
        public void Buscar(string nombre, NodoVector Padre, Delegate Comparacion)
        {
            NodoArbolB Aux = new NodoArbolB();
            bool Encontrado = true;
            if (Padre != null)
            {
                int num = HasNode(Padre.Vector);
                if (Padre != null)
                {
                    for (int i = 0; i < num && Encontrado; i++)
                    {
                        if (Padre.Vector[i] != null)
                        {
                            int Comp = Convert.ToInt32(Comparacion.DynamicInvoke(nombre, Padre.Vector[i].Nombre));
                            if (Comp == 0)
                            {
                                Encontrado = false;
                                Aux.Nuevo = Padre.Vector[i].Nuevo;
                                NuevaLista.Add(Aux.Nuevo);
                                if (Padre.Vector[i].Izquierda != null)
                                {
                                    Buscar(nombre, Padre.Vector[i].Izquierda, Comparacion);
                                }

                            }
                            else if (Comp < 0)
                            {
                                Buscar(nombre, Padre.Vector[i].Izquierda, Comparacion);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }
        }
        public int HasNode(NodoArbolB[] Verificar)
        {
            int i = 0;
            return HasNode(Verificar, i);
        }

        public int HasNode(NodoArbolB[] Verificar, int Contador)
        {
            if (Contador == Degree - 1)
            {
                return Contador;
            }
            else
            {
                if (Verificar[Contador] != null)
                {
                    return HasNode(Verificar, Contador + 1);
                }
                else
                {
                    return Contador;
                }
            }
        }
    }
}
