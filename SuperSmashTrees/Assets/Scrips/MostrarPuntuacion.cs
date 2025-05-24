using UnityEngine;
using TMPro;
using System.IO;

public class MostrarPuntuacion : MonoBehaviour
{
    public TextMeshProUGUI puntuacionText; // Referencia al componente TextMeshProUGUI para mostrar la puntuación
    public string nombreArchivo = "puntajes.txt"; // Nombre del archivo de puntajes

    private string rutaArchivo;

    void Start()
    {
        // Ruta completa al archivo (puedes ajustar según dónde lo guardes)
        rutaArchivo = Path.Combine(Application.dataPath, "Resources", nombreArchivo);
        ActualizarPuntajes();
    }

    public void ActualizarPuntajes()
    {
        if (File.Exists(rutaArchivo))
        {
            string puntajes = File.ReadAllText(rutaArchivo);
            puntuacionText.text = puntajes;
        }
        else
        {
            puntuacionText.text = "No se encontró el archivo de puntajes.";
        }
    }
}