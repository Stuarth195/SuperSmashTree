using UnityEngine;

public class UniqueTagAssigner : MonoBehaviour
{
    [SerializeField] private string baseTag = "Player";

    void Start()
    {
        string finalTag = baseTag;
        int counter = 1;

        // Verifica si ya hay un objeto con ese tag
        while (GameObject.FindGameObjectWithTag(finalTag) != null &&
               GameObject.FindGameObjectWithTag(finalTag) != this.gameObject)
        {
            finalTag = baseTag + counter;
            counter++;
        }

        // Verifica que el tag existe en el proyecto antes de asignarlo
        if (TagExists(finalTag))
        {
            gameObject.tag = finalTag;
            Debug.Log($"Tag asignado: {finalTag}");
        }
        else
        {
            Debug.LogError($"El tag '{finalTag}' no existe. Agrégalo manualmente en el Tag Manager.");
        }
    }

    private bool TagExists(string tag)
    {
        try
        {
            // Esto lanza excepción si el tag no existe
            GameObject.FindWithTag(tag);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
