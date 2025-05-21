using UnityEngine;
using System.Collections.Generic;
using DLLForUnityDB;
using Nodos;
using JetBrains.Annotations;

public class STModelo : MonoBehaviour
{
    public int NumPlayer;
    public string Reto;
    public Cola<ListaSimple<int>> ColaTagNum;

    public ListaSimple<Nodo<int>> ListaNodos;
    bool Terminado = false;


}
