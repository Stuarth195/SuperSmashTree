// InputHandler.cs
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject spawnerPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(spawnerPrefab); // Instanciar desde prefab
            Debug.Log("Spawner de esferas creado");
        }
    }
}