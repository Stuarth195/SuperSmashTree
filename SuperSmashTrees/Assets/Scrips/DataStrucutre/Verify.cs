using UnityEngine;
using Nodos;

public static class Verify
{
    public static int MedirNivel(Nodo node)
    {
        if (node == null) return 0;
        int leftHeight = MedirNivel(node.Left);
        int rightHeight = MedirNivel(node.Right);
        return Mathf.Max(leftHeight, rightHeight) + 1;
    }
}
