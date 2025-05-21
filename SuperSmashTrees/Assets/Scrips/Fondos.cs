using UnityEngine;

public class ImagenAleatoria3D : MonoBehaviour
{
    [Header("Objeto 3D al que se cambiará la textura (ej: cubo)")]
    public GameObject objetoDestino;

    [Header("Carpeta dentro de Resources (ej: 'Fondos')")]
    public string carpeta = "Fondos";

    void Start()
    {
        if (objetoDestino == null)
        {
            Debug.LogError("No se ha asignado ningún objeto.");
            return;
        }

        // Carga todas las texturas dentro de Resources/carpeta
        Texture2D[] texturas = Resources.LoadAll<Texture2D>(carpeta);

        if (texturas.Length == 0)
        {
            Debug.LogError($"No se encontraron texturas en Resources/{carpeta}");
            return;
        }

        // Selecciona una textura aleatoria
        Texture2D texturaSeleccionada = texturas[Random.Range(0, texturas.Length)];

        // Asignar textura al material del objeto
        Renderer renderer = objetoDestino.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = texturaSeleccionada;
        }
        else
        {
            Debug.LogError("El objeto no tiene un Renderer.");
        }
    }
}
