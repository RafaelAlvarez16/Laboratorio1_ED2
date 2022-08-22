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

        public void Insert(NodoArbolB<T> NewNodo, NodoVector<T> VectorPadre, Delegate Comparacion)
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
                NodoArbolB<T> Auxiliar = new NodoArbolB<T>();
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
                            NodoVector<T> Nuevo = new NodoVector<T>(Degree);
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
                int Comp = 0;
                bool VerificacionDeEntrada = true;
                for (int i = 0; i <= Degree - 2 && VerificacionDeEntrada; i++)
                {
                    Comp = Convert.ToInt32(Comparacion.DynamicInvoke(NewNodo.Data, VectorPadre.Vector[i].Data));
                    if (Comp < 0)
                    {
                        Comp = -1;
                        VerificacionDeEntrada = false;
                        Insert(NewNodo, VectorPadre.Vector[i].Izquierda, Comparacion);
                        VectorPadre.Vector[i].Izquierda.Padre = VectorPadre;
                    }
                    else
                    {
                        if (VectorPadre.Vector[i+1] != null)
                        {
                            VerificacionDeEntrada = true;
                        }
                        else
                        {
                            VerificacionDeEntrada = false;
                            Insert(NewNodo, VectorPadre.Vector[i].Derecha, Comparacion);
                            if (VectorPadre.Vector[i]!=null)
                            {
                                VectorPadre.Vector[i].Derecha.Padre = VectorPadre;
                            }
                        }
                    }
                }

            }
        }


        //Insertar Un desvordamiento que no es la Raiz 
        public void InsertNotRoot(NodoArbolB<T> NodeToInsert, NodoVector<T> Padre, Delegate Comparacion)
        {
            bool Ciclo = true;
            int Rangos = 0;
            NodoArbolB<T> Aux = new NodoArbolB<T>();
            for (int i = 0; i <= Degree - 1 && Ciclo == true; i++)
            {
                if (Padre.Vector[i] == null)
                {
                    Padre.Vector[i] = NodeToInsert;
                    Rangos = i; 
                    Ciclo = false;
                }
            }
            int Comp = Convert.ToInt16(Comparacion.DynamicInvoke(NodeToInsert.Data,Padre.Vector[0].Data));
            Padre.Vector = ShellSort(Padre.Vector, Comparacion);
            if (Comp < 0)
            {
                Padre.Vector[Rangos].Izquierda = Padre.Vector[Rangos-1].Derecha;
            }
            else if (Rangos == Degree - 1)
            {
                if (FullVector(Raiz.Vector))//el padre.padre es nulo por lo que el vector no existe
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

        //Vaciar vector
        public NodoArbolB<T>[] VaciarVector(NodoArbolB<T>[] NodoAVaciar)
        {
            for (int i = 0; i <= Degree - 1; i++)
            {
                NodoAVaciar[i] = null;
            }
            return NodoAVaciar;
        }

        // Llenado del Vector en la posicion que se dice
        /*NodoArbolB<T>[] SheetFilling(T New, NodoArbolB<T>[] Capsule,Delegate Condicion) 
        {
            return SheetFilling(New, Capsule, 0, Condicion);
        }
        // Procedimiento de busqueda de Indice
        NodoArbolB<T>[] SheetFilling(T New, NodoArbolB<T>[] Capsule, int IndexVector, Delegate Condicion)
        {
            if (Capsule[IndexVector].Data == null)
            {
                Capsule[IndexVector].Data = New;
                ShellSort(Capsule,Condicion);
                return Capsule;
            }
            else 
            {
                return SheetFilling(New, Capsule, IndexVector+1,Condicion);
            }
        }
        */
        //Ordenar el Vector 
        public NodoArbolB<T>[] ShellSort(NodoArbolB<T>[] Ordenar, Delegate Condicion)
        {
            for (int k = 0; k < Degree - 1; k++)
            {
                for (int f = 0; f < (Degree - 1) - k; f++)
                {
                    if (Ordenar[f] != null && Ordenar[f + 1] != null)
                    {
                        int Comparar = Convert.ToInt32(Condicion.DynamicInvoke(Ordenar[f].Data, Ordenar[f + 1].Data));
                        if (Comparar > 0)
                        {
                            NodoArbolB<T> aux;
                            aux = Ordenar[f];
                            Ordenar[f] = Ordenar[f + 1];
                            Ordenar[f + 1] = aux;
                        }
                    }
                }
            }
            return Ordenar;
        }
        // Separacion de Nodo por desvordamiento
        public NodoArbolB<T> UploadNode(NodoArbolB<T>[] UploadVector)
        {
            NodoArbolB<T> Aux = new NodoArbolB<T>();
            NodoVector<T> Left = new NodoVector<T>(Degree);
            NodoVector<T> Right = new NodoVector<T>(Degree);
            int Div = 0;
            if (Impar())
            {
                Div = (Degree / 2) - 1;
                Aux = UploadVector[Div];
            }
            else 
            {
                Div = (Degree / 2);
                Aux = UploadVector[Div];
            }

            for (int i = 0; i < Div; i++)
            {
                Left.Vector[i] = UploadVector[i];
            }
            int Begin = 0;
            for (int i = Degree - 1; i > Div; i--)
            {
                Right.Vector[Begin] = UploadVector[i];
                Begin++;
            }

            Aux = UploadVector[Div];


            Aux.Izquierda = Left;
            Aux.Derecha = Right;

            return Aux;
        }


        // Verifica si el nodo desvordado es la raiz
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




        // Verifica si en verdad tiene todos los espacios del vector llenos
        public bool FullVector(NodoArbolB<T>[] Verificar)
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

        public int HasNode(NodoArbolB<T>[] Verificar)
        {
            int i = 0;
            return HasNode(Verificar, i);
        }

        public int HasNode(NodoArbolB<T>[] Verificar, int Contador)
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

        public void Delete(object New, NodoVector<T> Capsule, Delegate Comparacion)
        {

            bool verificar = true;
            for (int i = 0; i < Capsule.Vector.Length-1 && verificar; i++)
            {
                int comp = Convert.ToInt32(Comparacion.DynamicInvoke(New, Capsule.Vector[i].Data));
                if (comp==0)
                {
                    verificar = false;
                    DeleteForm(i, Capsule, Comparacion);
                }
                else if (comp<0)
                {
                    if (Capsule.Vector[i].Izquierda!=null)
                    {
                        verificar = false;
                        Delete(New, Capsule.Vector[i].Izquierda, Comparacion);
                    }
                }
                else
                {
                    if (Capsule.Vector[i+1]==null)
                    {
                        verificar = false;
                        Delete(New, Capsule.Vector[i].Derecha, Comparacion);
                    }
                    else
                    {
                    }
                }

            }
        }
        public void DeleteForm(int num, NodoVector<T> Vector, Delegate Comparacion)
        {
            if (Vector.Vector[num].Derecha==null && Vector.Vector[num].Izquierda == null)//Verificar si no tiene hijos
            {
                if (Vector.Padre==null)//Verificar si no tiene padre
                {
                    if (Vector.Vector[num+1]==null)
                    {
                        Vector.Vector[num] = null;
                    }
                    else
                    {
                        Vector.Vector[num] = null;
                        OrdenarEspacios(Vector, num);
                    }
                }
                else// Es un vector sin hijos pero con un padre HOJA
                {
                    int Minposible = 0;
                    int PoseeHojas = 0;
                    if (Impar())
                    {
                        Minposible = (Degree / 2)-1;
                    }
                    else
                    {
                        Minposible = (Degree / 2);
                    }

                    for (int i = 0; i < Degree-2; i++)
                    {
                        if (Vector.Vector[i]!=null)
                        {
                            PoseeHojas++;
                        }
                    }
                    if (PoseeHojas==Minposible)//posee lo mismo de las hojas
                    {
                            VerHermanosMinimo(Vector, Comparacion, Minposible, num); ////////////
                    }
                    else if (Minposible<PoseeHojas)//tiene mas del minimo en las hojas
                    {
                        Vector.Vector[num] = null;
                        OrdenarEspacios(Vector, num);
                    }
                    else{}
                }
            }// si tiene hijos
            else
            {
                if (Vector.Padre == null)//Ver si tiene padre
                {
                    if (Vector.Vector[1]==null)
                    {
                        int hijosizqmas = 0;
                        int hijosizqmen = 0;
                        T aux = default;
                        NodoArbolB<T> Auxiliar = new NodoArbolB<T>();
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

                        if (hijosizqmen>hijosizqmas)
                        {
                            Vector.Vector[0].Data = Vector.Vector[0].Izquierda.Vector[hijosizqmen-1].Data;
                            Vector.Vector[0].Izquierda.Vector[hijosizqmen-1] = null;
                        }
                        else if (hijosizqmas>hijosizqmen)
                        {
                            Vector.Vector[0].Data = Vector.Vector[0].Derecha.Vector[0].Data;
                            Vector.Vector[0].Derecha.Vector[0] = null;
                            OrdenarEspacios(Vector.Vector[0].Derecha, 0);
                        }
                        else
                        {
                            for (int i = 0; i < hijosizqmen; i++)
                            {
                                Vector.Vector[i] = Vector.Vector[i].Izquierda.Vector[i];
                            }
                            for (int i = hijosizqmas-1; i < Vector.Vector.Length; i++)
                            {
                                if (hijosizqmas>0)
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

        public void VerHermanosMinimo(NodoVector<T> Vector, Delegate Comparision, int Minposible, int numEliminar) 
        {
            NodoArbolB<T>[] VectorPadre = Vector.Padre.Vector;
            bool verificacion = true;
            int numpa = 0;//el hijo donde se encuentra el que se desea eliminar
            int espacioPa = 0;//numero que indica en que espacio del vector se encuentra el padre que se esta manejando
            int PoseeEnVec = 0;//
            for (int i = 0; i <= Vector.Padre.Vector.Length -1 && verificacion ; i++)//buscar el espacio del data padre
            {
                int compa = Convert.ToInt32(Comparision.DynamicInvoke(Vector.Vector[0].Data, VectorPadre[i].Data));
                if (compa<0)
                {
                    verificacion = false;
                    espacioPa = i;
                    numpa = -1; //Se encuentra en el hijo izquierdo 
                }
                else if(VectorPadre[i+1]==null)
                {
                    verificacion = false;
                    espacioPa = i;
                    numpa = 1; //Se encuentra en el hijo derecho
                }
            }

            if (numpa==-1)
            {
                if (espacioPa==0)//es el primero y el vector solo tiene un padre que comparte hermano
                {
                    for (int i = 0; i <= Vector.Vector.Length-1; i++)
                    {
                        if (VectorPadre[espacioPa].Derecha.Vector[i]!=null)
                        {
                            PoseeEnVec++;
                        }
                    }
                    if (PoseeEnVec>Minposible)
                    {
                        T aux=default;
                        aux=VectorPadre[espacioPa].Data;
                        VectorPadre[espacioPa].Data = VectorPadre[espacioPa].Derecha.Vector[0].Data;
                        NodoArbolB<T> Auxiliar = new NodoArbolB<T>();
                        Auxiliar.Data = aux;
                        VectorPadre[espacioPa].Derecha.Vector[0] = null;
                        Vector.Vector[numEliminar] = null;
                        Vector.Vector[Minposible] = Auxiliar;
                        OrdenarEspacios(Vector, numEliminar);
                        OrdenarEspacios(VectorPadre[espacioPa].Derecha, 0);
                    }
                    else
                    {
                        if (VectorPadre[espacioPa + 1]!=null)
                        {
                            Vector.Vector[numEliminar] = null;
                            NodoArbolB<T> Auxiliar = new NodoArbolB<T>();
                            T aux = default;
                            aux = VectorPadre[espacioPa].Data;
                            VectorPadre[espacioPa] = null;
                            Auxiliar.Data = aux;
                            VectorPadre[espacioPa + 1].Izquierda.Vector[1] = Auxiliar;
                            OrdenarEspacios(Vector.Padre, 0);
                            ShellSort(VectorPadre[espacioPa].Izquierda.Vector, Comparision);
                        }
                    }
                }
                else //esta entre los nodos de en medio o de ultimo
                {
                    int hijosizqmas = 0;
                    int hijosizqmen = 0;
                    for (int i = 0; i < VectorPadre.Length; i++)
                    {
                        if (VectorPadre[espacioPa - 1].Izquierda.Vector[i] != null)
                        {
                            hijosizqmen++;
                        }
                        if (VectorPadre[espacioPa + 1]== null)
                        {
                            if (VectorPadre[espacioPa].Derecha.Vector[i] != null)
                            {
                                hijosizqmas++;
                            }
                        }
                    }
                    if (hijosizqmen > Minposible)
                    {
                        ShellSort(VectorPadre[espacioPa-1].Izquierda.Vector, Comparision);
                        T aux = default;
                        aux = VectorPadre[espacioPa-1].Data;
                        VectorPadre[espacioPa-1].Data = VectorPadre[espacioPa-1].Izquierda.Vector[hijosizqmen-1].Data;
                        VectorPadre[espacioPa - 1].Izquierda.Vector[hijosizqmen - 1] = null;
                        NodoArbolB<T> Auxiliar = new NodoArbolB<T>();
                        Auxiliar.Data = aux;
                        VectorPadre[espacioPa].Izquierda.Vector[Minposible] = Auxiliar;
                        Vector.Vector[numEliminar] = null;
                        OrdenarEspacios(Vector, numEliminar);
                    }
                    else if (hijosizqmas > Minposible)
                    {
                        ShellSort(VectorPadre[espacioPa].Derecha.Vector, Comparision);
                        T aux = default;
                        aux = VectorPadre[espacioPa].Data;
                        VectorPadre[espacioPa ].Data = VectorPadre[espacioPa].Derecha.Vector[0].Data;
                        VectorPadre[espacioPa ].Derecha.Vector[0 ] = null;
                        NodoArbolB<T> Auxiliar = new NodoArbolB<T>();
                        Auxiliar.Data = aux;
                        VectorPadre[espacioPa].Izquierda.Vector[Minposible] = Auxiliar;
                        Vector.Vector[numEliminar] = null;
                        OrdenarEspacios(Vector, numEliminar);
                        OrdenarEspacios(VectorPadre[espacioPa].Derecha, numEliminar);

                    }
                }
            }//derecho
            else//el hijo derecho con su padre 
            {
            }
        }
        public void OrdenarEspacios(NodoVector<T> Vector, int num) 
        {
            bool verificacion = true;
            for (int i = num; i < Vector.Vector.Length-2 && verificacion; i++)
            {
                NodoArbolB<T> Aux = null;
                if (Vector.Vector[i+1]!=null)
                {
                    Aux = Vector.Vector[i + 1];
                    Vector.Vector[i] = Aux;
                    Vector.Vector[i + 1] = null;
                }
            }
        }
        public T BuscarData(object Buscar,NodoVector<T> Padre, Delegate Comparacion)
        {
            NodoArbolB<T> Aux = new NodoArbolB<T>();
            if (Raiz != null)
            {
                if (Padre != null)
                {
                    bool Encontrado = true;
                    for (int i = 0; i <= HasNode(Padre.Vector) && Encontrado; i++)
                    {
                        if (Padre.Vector[i] != null)
                        {
                            int Comp = Convert.ToInt32(Comparacion.DynamicInvoke(Buscar, Padre.Vector[i].Data));
                            if (Comp == 0)
                            {
                                Encontrado = false;
                                Aux.Data = Padre.Vector[i].Data;
                            }
                            else if (Comp < 0)
                            {
                                Encontrado = false;
                                Aux.Data = BuscarData(Buscar, Padre.Vector[i].Izquierda, Comparacion);
                            }
                            else
                            {
                                if(HasNode(Padre.Vector) == i)
                                    Aux.Data = BuscarData(Buscar, Padre.Vector[i].Derecha, Comparacion);
                            }
                        }
                        else
                        {
                            Encontrado = false;
                            return Aux.Data;
                        }
                    }
                    return Aux.Data;
                }
                else 
                {
                    return Aux.Data;
                }
            }
            else 
            {
                return Aux.Data;
            }
        }
        public void EliminarArbol() 
        {
            Raiz = null;
        }
    }
}