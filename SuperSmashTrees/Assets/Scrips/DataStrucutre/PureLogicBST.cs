using UnityEngine;
using Nodos;

public class PureLogicBST 
{
    // Método para insertar un nuevo nodo en el árbol
    private Nodo InsertRec(Nodo root, int value)
    {
        if (root == null) return new Nodo(value);
        if (value == root.value)
            return root; // No duplicados
        if (value < root.value)
            root.Left = InsertRec(root.Left, value);
        else if (value > root.value)
            root.Right = InsertRec(root.Right, value);
        return root;
    }
    // Método para buscar un valor en el árbol
    public bool Search(Nodo root, int value)
    {
        return SearchRec(root, value);
    }
    // Método recursivo para buscar un valor
    private bool SearchRec(Nodo root, int value)
    {
        if (root == null)
            return false; 
        if (root.value == value)
            return true;
        if (value < root.value)
        return SearchRec(root.Left, value);
        else
        return SearchRec(root.Right, value);

    }
}