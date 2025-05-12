using UnityEngine;
using TMPro;

/// <summary>
/// Fábrica especializada en crear nodos visuales para un árbol.
/// Hereda de ObjectFactory.
/// </summary>
public class TreeNodeFactory : ObjectFactory
{
    /// <summary>
    /// Crea un nodo en la posición indicada y le asigna un valor visual.
    /// </summary>
    /// <param name="position">Posición en la escena donde aparece el nodo.</param>
    /// <param name="value">Valor numérico que representa el nodo.</param>
    public GameObject CreateTreeNode(Vector3 position, int value)
    {
        GameObject node = CreateObject(position);
        TMP_Text text = node.GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            text.text = value.ToString();
        }
        node.name = $"Nodo_{value}";
        return node;
    }

    /// <summary>
    /// Método llamado automáticamente por ObjectFactory para configuraciones iniciales.
    /// </summary>
    protected override void ConfigureObject(GameObject obj)
    {
        // Por ahora, el ,método está vacío, pero se puede usar para configuraciones adicionales.
    }
}
