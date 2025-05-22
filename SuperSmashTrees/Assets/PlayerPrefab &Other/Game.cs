using System.Collections;
using UnityEngine;
using DLLForUnityStandart;
using Nodos;

public class Game : MonoBehaviour
{
    public string tagRoot = "Player"; // Raíz para los tags (ej: "Player1", "Player2", ...)
    STModelo gamemanager = STModelo.Instance;
    public void PlayerJoined(Player p)
    {
        
        // Asignar un color aleatorio
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        p.ChangeColor(randomColor);

        // Iniciar desde Player1
        int index = 1;
        string candidateTag;

        // CREA UN NODO POR PLAYER QUE SE CREA
        
        

        while (true)
        {
            candidateTag = tagRoot + index;

            GameObject[] objectsWithTag;
            try
            {
                objectsWithTag = GameObject.FindGameObjectsWithTag(candidateTag);
            }
            catch
            {
                // El tag no existe en el Tag Manager
                Debug.LogWarning($"El tag '{candidateTag}' no existe. Asegúrate de crearlo en el Tag Manager.");
                return;
            }

            if (objectsWithTag.Length == 0)
            {
                break; // Tag libre
            }

            index++;
        }

        // Asignar el tag libre al objeto del jugador
        p.gameObject.tag = candidateTag;
        Debug.Log($"Tag asignado al jugador: {candidateTag}");
    }
}
