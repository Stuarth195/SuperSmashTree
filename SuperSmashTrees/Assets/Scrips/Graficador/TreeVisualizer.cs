using UnityEngine;
using Nodos;

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

        // Asegúrate de que el contenedor esté en la escena activa
        nodesParent.SetParent(this.transform, false);

        // Establece la posición inicial del contenedor
        nodesParent.position = new Vector3(0, 5, 0); // Cambia esta posición según sea necesario
    }
    /// <summary>
    /// Visualiza el árbol completo dado su nodo raíz.
    /// </summary>
    public void VisualizeTree(Nodo root)
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
    private void VisualizeRecursive(Nodo node, Vector3 position, int depth, int direction)
    {
        if (node == null) return;

        // Calcula la posición horizontal basada en la profundidad
        float offset = horizontalSpacing * Mathf.Pow(0.5f, depth);

        // Ajusta la posición en X dependiendo si es hijo izquierdo o derecho
        position.x += direction * offset;
        position.y = -depth * verticalSpacing;

        // Crea el nodo visual
        GameObject nodeVisual = nodeFactory.CreateTreeNode(position, node.value);

        nodeVisual.transform.SetParent(nodesParent, false);

        // Dibuja la rama hacia el hijo izquierdo
        if (node.Left != null)
        {
            Vector3 leftPos = position + new Vector3(-offset, -verticalSpacing, 0);
            DrawLine(position, leftPos);
            VisualizeRecursive(node.Left, leftPos, depth + 1, -1);
        }

        // Dibuja la rama hacia el hijo derecho
        if (node.Right != null)
        {
            Vector3 rightPos = position + new Vector3(offset, -verticalSpacing, 0);
            DrawLine(position, rightPos);
            VisualizeRecursive(node.Right, rightPos, depth + 1, 1);
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

    public void ClearVisualization()
    {
        // Elimina todos los objetos hijos del visualizador (nodos y líneas)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
