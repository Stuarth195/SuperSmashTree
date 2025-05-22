using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReaparecerPorCaida : MonoBehaviour
{
    [Header("Configuración de caída")]
    public float alturaMinimaY = -10f;
    public float tiempoDeEspera = 3f;
    public Vector3 posicionDeRespawn = new Vector3(0, 1, 0);

    [Header("UI generada automáticamente")]
    public Camera camaraUI;                      // Asigna aquí la cámara principal si no la detecta sola
    public Vector2 mensajeOffset = new Vector2(0, 100); // Offset sobre el jugador

    private Canvas canvas;
    private Text mensajeUI;
    private RectTransform canvasRect;
    private bool estaReapareciendo = false;
    private static int jugadoresReapareciendo = 0;

    void Start()
    {
        if (camaraUI == null)
            camaraUI = Camera.main;

        CrearCanvasYMensajeUI();
    }

    void Update()
    {
        if (!estaReapareciendo && transform.position.y < alturaMinimaY)
        {
            StartCoroutine(ReaparecerConMensaje());
        }
    }

    IEnumerator ReaparecerConMensaje()
    {
        estaReapareciendo = true;
        jugadoresReapareciendo++;

        mensajeUI.enabled = true;

        float tiempoRestante = tiempoDeEspera;

        while (tiempoRestante > 0f)
        {
            // Actualizar posición en pantalla
            Vector3 screenPos = camaraUI.WorldToScreenPoint(transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, screenPos, camaraUI, out Vector2 localPoint);
            mensajeUI.rectTransform.anchoredPosition = localPoint + mensajeOffset;

            // Mostrar mensaje
            mensajeUI.text = $"Respawn de {gameObject.name} en {tiempoRestante:F1}s";

            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

        transform.position = posicionDeRespawn;

        mensajeUI.enabled = false;
        mensajeUI.text = "";

        jugadoresReapareciendo--;
        estaReapareciendo = false;
    }

    void CrearCanvasYMensajeUI()
    {
        // Crear Canvas si no existe
        GameObject canvasGO = new GameObject($"Canvas_{gameObject.name}");
        canvasGO.layer = LayerMask.NameToLayer("UI");
        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camaraUI;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();
        canvasRect = canvas.GetComponent<RectTransform>();

        // Crear Texto UI
        GameObject textGO = new GameObject("MensajeUI");
        textGO.transform.SetParent(canvasGO.transform);
        mensajeUI = textGO.AddComponent<Text>();

        // Estilo del texto
        mensajeUI.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        mensajeUI.fontSize = 24;
        mensajeUI.alignment = TextAnchor.MiddleCenter;
        mensajeUI.color = Color.white;
        mensajeUI.raycastTarget = false;
        mensajeUI.enabled = false;

        // Ajustes del RectTransform
        RectTransform rt = mensajeUI.rectTransform;
        rt.sizeDelta = new Vector2(400, 50);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
    }
}
