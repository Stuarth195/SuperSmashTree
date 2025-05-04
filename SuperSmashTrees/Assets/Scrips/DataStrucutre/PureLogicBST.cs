using UnityEngine;

public class PureLogicBST : MonoBehaviour
{
    public class Node : IBinaryTreeNode
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            Value = value;
        }

        public int GetValue() => Value;
        public IBinaryTreeNode GetLeft() => Left;
        public IBinaryTreeNode GetRight() => Right;
    }

    private Node root;

    public Node GetRoot() => root;

    public void Insert(int value)
    {
        root = InsertRec(root, value);
    }

    private Node InsertRec(Node root, int value)
    {
        if (root == null) return new Node(value);
        if (value < root.Value)
            root.Left = InsertRec(root.Left, value);
        else if (value > root.Value)
            root.Right = InsertRec(root.Right, value);
        return root;
    }

    public bool Search(int value)
    {
        return SearchRec(root, value);
    }

    private bool SearchRec(Node root, int value)
    {
        if (root == null) return false;
        if (root.Value == value) return true;
        return value < root.Value ? SearchRec(root.Left, value) : SearchRec(root.Right, value);
    }
}
