using UnityEngine;
using System.Collections.Generic;
using DLLForUnityStandart;
using Nodos;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class EsferaController : MonoBehaviour
{


    [Header("Configuración desde Inspector")]
    public float velocidadCaida = 5f;
    public int numeroMin = 1;
    public int numeroMax = 100;
    public float tamanoLetra = 5f;

    public int numeroEsfera; // Número aleatorio
    public Color colorEsfera; // Color aleatorio
    public ListaSimple<string> tagsValidos; // Lista de tags válidos 
    public STModelo gameManager; // Singleton que recibe la info

    void Start()
    {
        gameManager = STModelo.Instance;

        if (gameManager == null)
        {
            Debug.LogError("No se encontró el GameManager.");
            return;
        }
        else
        {
            Debug.LogWarning("✅ Se instancio Bien");
        }

        // Número aleatorio según rango configurado
        numeroEsfera = Random.Range(numeroMin, numeroMax);

        // Color aleatorio
        colorEsfera = new Color(Random.value, Random.value, Random.value);

        // Inicializar lista de tags válidos
        tagsValidos = new ListaSimple<string>();
        tagsValidos.Insertar("Player1");
        tagsValidos.Insertar("Player2");
        tagsValidos.Insertar("Player3");

        MostrarNumeroEnEsfera();
    }

    void OnTriggerEnter(Collider other)
    {
        List<string> tags = tagsValidos.Recorrer();
        foreach (string tag in tags)
        {
            if (other.CompareTag(tag))
            {
                int numeroJugador = gameManager.PlayerNum(tag);
                bool exito = false;

                while (!exito)
                {
                    try
                    {
                        gameManager.RecibirColision(numeroJugador, numeroEsfera);
                        Debug.LogWarning($"❌ coliciono {numeroJugador} con  esfera {numeroEsfera} ");
                        Destroy(gameObject);
                        exito = true;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error al procesar la colisión: {e.Message}");
                    }
                }

               

                break;
            }
        }
    }

    void MostrarNumeroEnEsfera()
    {
        TMP_Text texto = GetComponentInChildren<TMP_Text>();
        if (texto != null)
        {
            texto.text = numeroEsfera.ToString();
            texto.color = Color.black;
            texto.alignment = TextAlignmentOptions.Center;
            texto.fontSize = tamanoLetra;
        }
        else
        {
            Debug.LogWarning("No se encontró un componente TMP_Text en los hijos de la esfera.");
        }
    }

    void Update()
    {
        // Caída suave con velocidad configurable
        transform.Translate(Vector3.down * velocidadCaida * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
