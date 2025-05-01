using UnityEngine;

public class PureLogicBST : MonoBehaviour
{

    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private Node root;

    // Método que se encarga de hacer la inserción de un nuevo nodo en el árbol a nivel solo de la memoria
    // y no en la escena de Unity.
    public void Insert(int value)
    {
        root = InsertRec(root, value);
    }

    private Node InsertRec(Node root, int value)
    {
        if (root == null)
        {
            root = new Node(value);
            return root;
        }

        if (value < root.Value)
            root.Left = InsertRec(root.Left, value);
        else if (value > root.Value)
            root.Right = InsertRec(root.Right, value);

        return root;
    }

    // Search for a value in the BST
    public bool Search(int value)
    {
        return SearchRec(root, value);
    }

    private bool SearchRec(Node root, int value)
    {
        if (root == null)
            return false;

        if (root.Value == value)
            return true;

        if (value < root.Value)
            return SearchRec(root.Left, value);

        return SearchRec(root.Right, value);
    }
}

