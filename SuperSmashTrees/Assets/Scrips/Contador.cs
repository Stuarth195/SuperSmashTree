using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    public static Contador Instancia { get; private set; }

    [Header("Referencias UI")]
    public Canvas canvasPadre;       // Arrástralo en el Inspector
    public Text prefabMensajeUI;      // Un prefab de Text (UI) dentro de un Canvas

    void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Instancia un nuevo Text en el canvas, lo nombra y lo deja deshabilitado.
    /// </summary>
    public Text CrearMensajeUI(string nombreJugador)
    {
        Text nuevo = Instantiate(prefabMensajeUI, canvasPadre.transform);
        nuevo.name = $"Mensaje_{nombreJugador}";
        nuevo.text = "";
        nuevo.enabled = false;
        return nuevo;
    }
}
