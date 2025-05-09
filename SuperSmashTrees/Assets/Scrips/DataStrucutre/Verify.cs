using UnityEngine;
using BinaryTree;

public static class Verify
{
    public static int MedirNivel(this IBinaryTreeNode node)
    {
        if (node == null) return 0;
        int leftHeight = MedirNivel(node.GetLeft());
        int rightHeight = MedirNivel(node.GetRight());
        return Mathf.Max(leftHeight, rightHeight) + 1;
    }
}
