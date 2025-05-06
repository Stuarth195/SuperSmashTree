using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PantallaDePausa : MonoBehaviour
{
    public float opacidadFondo = 0.6f;
    public string textoPausa = "PAUSA";
    public int tamañoTexto = 100;
    public KeyCode teclaPausa = KeyCode.Return;

    private GameObject canvasPausa;
    private AudioSource[] audios;
    private float[] volumenesOriginales;
    private bool enPausa = false;

    void Start()
    {
        CrearPantallaDePausa();
        canvasPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaPausa))
        {
            if (enPausa)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    void PausarJuego()
    {
        Time.timeScale = 0f;

        audios = FindObjectsOfType<AudioSource>();
        volumenesOriginales = new float[audios.Length];
        for (int i = 0; i < audios.Length; i++)
        {
            volumenesOriginales[i] = audios[i].volume;
            audios[i].volume *= 0.2f; // Reduce el volumen al 20%
        }

        canvasPausa.SetActive(true);
        enPausa = true;
    }

    void ReanudarJuego()
    {
        Time.timeScale = 1f;

        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].volume = volumenesOriginales[i];
        }

        canvasPausa.SetActive(false);
        enPausa = false;
    }

    void CrearPantallaDePausa()
    {
        // Crear Canvas
        canvasPausa = new GameObject("CanvasPausa");
        var canvas = canvasPausa.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasPausa.AddComponent<CanvasScaler>();
        canvasPausa.AddComponent<GraphicRaycaster>();

        // Fondo oscuro
        GameObject fondo = new GameObject("FondoOscuro");
        fondo.transform.SetParent(canvasPausa.transform);
        var fondoImg = fondo.AddComponent<Image>();
        fondoImg.color = new Color(0, 0, 0, opacidadFondo);
        var rtFondo = fondo.GetComponent<RectTransform>();
        rtFondo.anchorMin = Vector2.zero;
        rtFondo.anchorMax = Vector2.one;
        rtFondo.offsetMin = Vector2.zero;
        rtFondo.offsetMax = Vector2.zero;

        // Texto PAUSA
        GameObject textoGO = new GameObject("TextoPausa");
        textoGO.transform.SetParent(canvasPausa.transform);
        var texto = textoGO.AddComponent<Text>();
        texto.text = textoPausa;
        texto.alignment = TextAnchor.MiddleCenter;
        texto.fontSize = tamañoTexto;
        texto.color = Color.white;
        texto.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        var rtTexto = texto.GetComponent<RectTransform>();
        rtTexto.anchorMin = new Vector2(0.5f, 0.5f);
        rtTexto.anchorMax = new Vector2(0.5f, 0.5f);
        rtTexto.anchoredPosition = Vector2.zero;
        rtTexto.sizeDelta = new Vector2(800, 200);
    }
}
