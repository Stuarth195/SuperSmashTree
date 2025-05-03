using UnityEngine;

public class PureLogicAVL
{
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;
        public int Height; // Altura del nodo

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Height = 1; // Inicialmente, la altura de un nuevo nodo es 1
        }
    }
    
    public class AVLTree
    {
        private Node root;

        // Método de llamada para  insertar un nuevo valor en el árbol AVL
        public void Insert(int value)
        {
            root = InsertRec(root, value);
        }

        private Node InsertRec(Node node, int value)
        {
            // Realiza la inserción normal en un árbol binario de búsqueda
            if (node == null)
                return new Node(value);

            if (value < node.Value)
                node.Left = InsertRec(node.Left, value);
            else if (value > node.Value)
                node.Right = InsertRec(node.Right, value);
            else // Duplicados no se permiten
                return node;

            // Actualiza la altura del nodo padre
            node.Height = 1 + Mathf.Max(GetHeight(node.Left), GetHeight(node.Right));

            // Obtiene el factor de equilibrio del nodo padre
            int balance = GetBalance(node);

            // Si el nodo se vuelve desbalanceado, hay 4 casos

            // Caso Izquierda Izquierda
            if (balance > 1 && value < node.Left.Value)
                return RightRotate(node);

            // Caso Derecha Derecha
            if (balance < -1 && value > node.Right.Value)
                return LeftRotate(node);

            // Caso Izquierda Derecha
            if (balance > 1 && value > node.Left.Value)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Caso Derecha Izquierda
            if (balance < -1 && value < node.Right.Value)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }

        private int GetBalance(Node node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private Node RightRotate(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            // Realiza la rotación
            x.Right = y;
            y.Left = T2;

            // Actualiza las alturas
            y.Height = Mathf.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Mathf.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            // Retorna el nuevo nodo raíz
            return x;
        }

        private Node LeftRotate(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            // Realiza la rotación
            y.Left = x;
            x.Right = T2;

            // Actualiza las alturas
            x.Height = Mathf.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Mathf.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            // Retorna el nuevo nodo raíz
            return y;
        }
    }
}
