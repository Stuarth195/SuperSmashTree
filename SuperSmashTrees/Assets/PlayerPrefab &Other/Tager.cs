using UnityEngine;

public class UniqueTagAssigner : MonoBehaviour
{
    private string[] allowedTags = { "Player1", "Player2", "Player3" };

    void Start()
    {
        foreach (string tag in allowedTags)
        {
            if (GameObject.FindGameObjectWithTag(tag) == null)
            {
                if (TagExists(tag))
                {
                    gameObject.tag = tag;
                    Debug.Log($"Tag asignado: {tag}");
                    return;
                }
                else
                {
                    Debug.LogError($"El tag '{tag}' no existe. Agrégalo en el Tag Manager.");
                    return;
                }
            }
        }

        Debug.LogWarning("Ya hay 3 jugadores activos. No se asignó ningún tag.");
    }

    private bool TagExists(string tag)
    {
        // Verifica si el tag está definido en el Tag Manager
        foreach (string definedTag in UnityEditorInternal.InternalEditorUtility.tags)
        {
            if (definedTag == tag) return true;
        }
        return false;
    }
}
