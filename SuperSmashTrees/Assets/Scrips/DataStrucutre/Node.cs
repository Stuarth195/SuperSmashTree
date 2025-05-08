using UnityEngine;
using BinaryTree;

public class Node : IBinaryTreeNode
{
    public int Value;
    public Node Left;
    public Node Right;
    public int Height = 1;

    public Node(int value)
    {
        Value = value;
    }

    public int GetValue() => Value;
    public IBinaryTreeNode GetLeft() => Left;
    public IBinaryTreeNode GetRight() => Right;
}
