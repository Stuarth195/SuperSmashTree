using UnityEngine;

public class ImagenAleatoria3D : MonoBehaviour
{
    public GameObject objetoDestino; // El cubo u objeto 3D

    public string carpeta = "Fondos"; // Carpeta en Resources donde están las imágenes

    void Start()
    {
        if (objetoDestino == null)
        {
            Debug.LogError("No has asignado el objetoDestino en el inspector.");
            return;
        }

        Texture2D[] texturas = Resources.LoadAll<Texture2D>(carpeta);

        if (texturas.Length == 0)
        {
            Debug.LogError($"No se encontraron texturas en Resources/{carpeta}");
            return;
        }

        Texture2D texturaAleatoria = texturas[Random.Range(0, texturas.Length)];

        Renderer renderer = objetoDestino.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.mainTexture = texturaAleatoria;
        }
        else
        {
            Debug.LogError("El objeto no tiene Renderer.");
        }
    }
}
