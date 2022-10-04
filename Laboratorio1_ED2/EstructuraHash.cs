using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio1_ED2
{
    public class EstructuraHash
    {
        public string LZ78_Encode(string nuevos)
        {
            Dictionary<int, string> Diccionario = new Dictionary<int, string>();
            string nuevo =nuevos;
            char[] arr = nuevo.ToCharArray(0, nuevo.Length);
            List<string> Aux = new List<string>();
            string cadena = "";
            string cadenaaux = "";
            string CadenaCodificada = "";
            int countdiccionario = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                cadena = arr[i].ToString();
                cadenaaux = arr[i].ToString();
                if (i == 0)
                {
                    Diccionario.Add(1, "0" + "_" + arr[0].ToString());
                    Aux.Add(arr[0].ToString());
                    countdiccionario++;
                }
                else if (VerificarsiExiste(Aux, arr[i].ToString()))//entra si existe
                {

                    int cont = 1;
                    for (int j = i; j < arr.Length; j++)
                    {
                        if (j + 1 < arr.Length)
                        {
                            cadena += arr[j + 1].ToString();
                            if (VerificarsiExiste(Aux, cadena))
                            {
                                cadenaaux += arr[j + 1].ToString();
                                cont++;
                            }
                            else
                            {
                                int posicion = VerificarPosicion(Aux, cadenaaux);
                                Diccionario.Add(countdiccionario, (posicion + 1).ToString() + "_" + arr[j + 1].ToString());
                                Aux.Add(cadena);
                                j = arr.Length;
                                i += cont;
                                countdiccionario++;
                                if (countdiccionario == 8)
                                {
                                    int asd = 0;
                                }
                            }
                        }
                        else
                        {
                            int posicion = VerificarPosicion(Aux, cadenaaux);
                            Diccionario.Add(countdiccionario, (posicion + 1).ToString() + "_" + " ");
                            Aux.Add(cadena);
                            j = arr.Length;
                            i += cont;
                            countdiccionario++;
                        }
                    }

                }
                else //entro si no existe dentro del diccionario
                {
                    Diccionario.Add(countdiccionario, "0" + "_" + arr[i].ToString());
                    Aux.Add(arr[i].ToString());
                    countdiccionario++;
                }
            }
            for (int i = 0; i < Diccionario.Count; i++)
            {
                CadenaCodificada += Diccionario[i + 1] + "|";
            }
            return CadenaCodificada;
        }

        public bool VerificarsiExiste(List<string> arreglo, string letra)
        {
            for (int i = 0; i < arreglo.Count; i++)
            {
                if (arreglo[i].ToString() == letra)
                {
                    return true;
                }
            }
            return false;
        }

        public int VerificarPosicion(List<string> arreglo, string letra)
        {
            for (int i = 0; i < arreglo.Count; i++)
            {
                if (arreglo[i].ToString() == letra)
                {
                    return i;
                }
            }
            return default;
        }
        public string LZ78_Dencode(string CadenaCodificada)
        {
            var values = CadenaCodificada.Split('|');// se le quita el ultimo digito 
            Dictionary<int, string> Diccionario = new Dictionary<int, string>();
            string[] valoressep = new string[values.Length-1];
            string CadenaDecodificada = "";
            for (int i = 0; i < values.Length-1 ; i++)
            {
                Diccionario.Add(i+1,values[i]);
            }
            for (int i = 0; i < Diccionario.Count; i++)
            {
                var val = Diccionario[i+1].Split("_");
                if (val[0]=="0")
                {
                    valoressep[i] = val[1];
                    CadenaDecodificada += val[1];
                }
                else
                {
                    valoressep[i] = valoressep[Int32.Parse(val[0]) - 1] + val[1];
                    CadenaDecodificada += valoressep[Int32.Parse(val[0])-1]+val[1];
                }
            }
            return CadenaDecodificada;
        }
    }
}
