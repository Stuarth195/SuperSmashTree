using UnityEngine;
using UnityEngine.UI;

public class BotonInicio : MonoBehaviour
{
    public Button botonInicio;
    public Timer timer; // Asigna el Timer desde el inspector
    public STModelo gameManager; // Asigna el STModelo desde el inspector

    void Start()
    {
        botonInicio.onClick.AddListener(IniciarJuego);
    }

    void IniciarJuego()
    {
        gameManager.StartGameButon(); // Inicia el juego (bolas empiezan a caer)
        timer.IniciarCronometro();    // Inicia el cronómetro
        botonInicio.gameObject.SetActive(false); // Oculta el botón
    }
}