using UnityEngine;
using System.Collections;
using DLLForUnityStandart;
using System;
using Random = UnityEngine.Random;
public class EsferaSpawner : MonoBehaviour
{
    [Header("Configuración desde Inspector")]
    STModelo GameManager; 
    public GameObject prefabEsfera;
    public float tiempoMin = 1f;
    public float tiempoMax = 3f;
    public float alturaSpawn = 10f;
    public Vector2 rangoX = new Vector2(-10f, 10f);
    public Vector2 rangoZ = new Vector2(-2f, 2f);

    void Start()
    {
        GameManager = STModelo.Instance;

        if (GameManager != null)
        {
            Debug.LogWarning("✅se instancio");
        }
        else
        {
            Debug.LogWarning("❌no se pudo instanciar");
        }

        StartCoroutine(ControladorDeSpawning());
    }

    IEnumerator ControladorDeSpawning()
    {
        while (true) // ciclo infinito que monitorea el estado de OnGame
        {
            if (GameManager.OnGame)
            {
                float espera = Random.Range(tiempoMin, tiempoMax);
                Vector3 posicion = new Vector3(
                    Random.Range(rangoX.x, rangoX.y),
                    alturaSpawn,
                    Random.Range(rangoZ.x, rangoZ.y)
                );

                Instantiate(prefabEsfera, posicion, Quaternion.identity);
                yield return new WaitForSeconds(espera);
            }
            else
            {
                // Esperar un momento antes de volver a comprobar el estado
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
