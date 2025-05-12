using UnityEngine;

public class PlayerCounter : MonoBehaviour
{
    [Tooltip("Lista de tags a revisar para contar jugadores")]
    public string[] playerTags;

    private int totalPlayers = 0;

    void Update()
    {
        totalPlayers = 0;

        foreach (string tag in playerTags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            totalPlayers += taggedObjects.Length;
        }

        Debug.Log($"Total de jugadores con los tags seleccionados: {totalPlayers}");
    }
}

