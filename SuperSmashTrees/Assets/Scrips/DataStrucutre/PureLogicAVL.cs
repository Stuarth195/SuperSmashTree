using UnityEngine;
using BinaryTree;


public class PureLogicAVL : IBinaryTree
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

    private Node InsertRec(Node node, int value)
    {
        if (node == null) return new Node(value);

        if (value < node.Value)
            node.Left = InsertRec(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRec(node.Right, value);
        else return node;

        node.Height = 1 + Mathf.Max(GetHeight(node.Left), GetHeight(node.Right));
        int balance = GetBalance(node);

        if (balance > 1 && value < node.Left.Value)
            return RightRotate(node);
        if (balance < -1 && value > node.Right.Value)
            return LeftRotate(node);
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    private int GetHeight(Node node) => node?.Height ?? 0;
    private int GetBalance(Node node) => GetHeight(node.Left) - GetHeight(node.Right);

    private Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Mathf.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
        x.Height = Mathf.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

        return x;
    }

    private Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Mathf.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
        y.Height = Mathf.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

        return y;
    }
}