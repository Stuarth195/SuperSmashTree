using UnityEngine;
using BinaryTree;
public class TreeVisualizer : MonoBehaviour
{
    public TreeNodeFactory nodeFactory; // Referencia al prefab y creación de nodos
    public float horizontalSpacing = 3f;
    public float verticalSpacing = 2f;

    private Transform nodesParent;

    private void Awake()
    {
        // Crea un contenedor para todos los nodos visuales
        GameObject container = new GameObject("VisualizedNodes");
        nodesParent = container.transform;
    }

    /// <summary>
    /// Visualiza el árbol completo dado su nodo raíz.
    /// </summary>
    public void VisualizeTree(IBinaryTreeNode root)
    {
        // Limpia la escena de visualizaciones anteriores
        foreach (Transform child in nodesParent)
        {
            Destroy(child.gameObject);
        }

        if (root != null)
        {
            VisualizeRecursive(root, Vector3.zero, 0, 0);
        }
    }

    /// <summary>
    /// Método recursivo para visualizar cada nodo.
    /// </summary>
    private void VisualizeRecursive(IBinaryTreeNode node, Vector3 position, int depth, int direction)
    {
        if (node == null) return;

        // Calcula la posición horizontal basada en la profundidad
        float offset = horizontalSpacing * Mathf.Pow(0.5f, depth);

        // Ajusta la posición en X dependiendo si es hijo izquierdo o derecho
        position.x += direction * offset;
        position.y = -depth * verticalSpacing;

        // Crea el nodo visual
        GameObject nodeVisual = nodeFactory.CreateTreeNode(position, node.GetValue());

        nodeVisual.transform.SetParent(nodesParent, false);

        // Dibuja la rama hacia el hijo izquierdo
        if (node.GetLeft() != null)
        {
            Vector3 leftPos = position + new Vector3(-offset, -verticalSpacing, 0);
            DrawLine(position, leftPos);
            VisualizeRecursive(node.GetLeft(), leftPos, depth + 1, -1);
        }

        // Dibuja la rama hacia el hijo derecho
        if (node.GetRight() != null)
        {
            Vector3 rightPos = position + new Vector3(offset, -verticalSpacing, 0);
            DrawLine(position, rightPos);
            VisualizeRecursive(node.GetRight(), rightPos, depth + 1, 1);
        }
    }

    /// <summary>
    /// Dibuja una línea entre dos puntos usando un LineRenderer.
    /// </summary>
    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("Line");
        lineObj.transform.SetParent(nodesParent, false);
        LineRenderer line = lineObj.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.05f;
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.startColor = Color.black;
        line.endColor = Color.black;
    }
}
