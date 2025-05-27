using UnityEngine;
using TMPro;

public class ActualizaTextoi : MonoBehaviour
{
    public STModelo gameManager; // Referencia al script GameManager
    public TextMeshProUGUI retoText; // Referencia al componente TextMeshProUGUI

    void Start()
    {
        gameManager = STModelo.Instance;
    }

    void Update()
    {
        if (gameManager.OnGame)
        {
            retoText.text = gameManager.MensajeDelReto;
        }
    }
}
