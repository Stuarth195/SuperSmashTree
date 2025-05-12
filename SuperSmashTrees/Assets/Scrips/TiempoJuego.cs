using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TemporizadorEnPantalla : MonoBehaviour
{
    public float duracionEnSegundos = 600f; // 10 minutos

    private Text textoTemporizador;
    private float tiempoRestante;

    [Header("Acción al finalizar")]
    public GameObject prefabConScript; // El prefab que se instanciará al finalizar
    public Vector3 posicionInstancia = Vector3.zero; // Posición donde instanciar el objeto

    void Start()
    {
        tiempoRestante = duracionEnSegundos;
        textoTemporizador = Contador.Instancia.CrearMensajeUI("Temporizador");

        textoTemporizador.alignment = TextAnchor.MiddleCenter;
        textoTemporizador.rectTransform.anchoredPosition = new Vector2(Screen.width / 2 - 150, -20); // Centrado arriba
        textoTemporizador.enabled = true;

        StartCoroutine(ActualizarTemporizador());
    }

    IEnumerator ActualizarTemporizador()
    {
        while (tiempoRestante > 0f)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            textoTemporizador.text = $"Tiempo restante: {minutos:D2}:{segundos:D2}";

            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

        textoTemporizador.text = "¡Tiempo agotado!";

        // Instanciar el objeto con el script
        if (prefabConScript != null)
        {
            Instantiate(prefabConScript, posicionInstancia, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No se asignó ningún prefab para instanciar al finalizar el temporizador.");
        }
    }
}
