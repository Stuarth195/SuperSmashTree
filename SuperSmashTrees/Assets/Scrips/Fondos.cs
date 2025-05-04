using UnityEngine;
using System.IO;

public class ImagenAleatoria3D : MonoBehaviour
{
    [Header("Objeto 3D al que se cambiará la textura (ej: cubo)")]
    public GameObject objetoDestino;

    [Header("Carpeta relativa dentro de Assets (ej: 'Fondos')")]
    public string carpeta = "Fondos";

    void Start()
    {
        if (objetoDestino == null)
        {
            Debug.LogError("No se ha asignado ningún objeto.");
            return;
        }

        string rutaAbsoluta = Path.Combine(Application.dataPath, carpeta);

        if (!Directory.Exists(rutaAbsoluta))
        {
            Debug.LogError("No se encontró la carpeta: " + rutaAbsoluta);
            return;
        }

        string[] extensiones = { "*.png", "*.jpg", "*.jpeg", "*.bmp" };
        var archivos = new System.Collections.Generic.List<string>();

        foreach (string ext in extensiones)
        {
            archivos.AddRange(Directory.GetFiles(rutaAbsoluta, ext));
        }

        if (archivos.Count == 0)
        {
            Debug.LogError("No se encontraron imágenes en la carpeta: " + rutaAbsoluta);
            return;
        }

        // Elegir imagen aleatoria
        string archivoSeleccionado = archivos[Random.Range(0, archivos.Count)];
        byte[] datos = File.ReadAllBytes(archivoSeleccionado);
        Texture2D textura = new Texture2D(2, 2);
        textura.LoadImage(datos);

        // Asignar textura al material del objeto
        Renderer renderer = objetoDestino.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = textura;
        }
        else
        {
            Debug.LogError("El objeto no tiene un Renderer.");
        }
    }
}
