using UnityEngine;
using BinaryTree;

public class PureLogicBST : IBinaryTree
{
    private Node root;

    public void Insert(int value)
    {
        root = InsertRec(root, value);
    }

    public IBinaryTreeNode GetRoot()
    {
        return root;
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