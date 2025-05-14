using UnityEngine;
using System.Collections.Generic;

public class RandomNumberSphereSpawner : MonoBehaviour
{
    [Header("Rango de posiciones")]
    public float minX = -10f, maxX = 10f;
    public float minY = 20f, maxY = 20f;
    public float minZ = -2f, maxZ = 2f;

    [Header("Rango de números aleatorios")]
    public int minNum = 0;
    public int maxNum = 99;

    [Header("Tiempo")]
    public float periodoTotalMinutos = 10f;
    public float intervaloMinSegundos = 1f;
    public float intervaloMaxSegundos = 5f;

    [Header("Tags a detectar en colisión")]
    public List<string> tagsADetectar;

    [Header("Configuración de la esfera")]
    public float tiempoDeDesaparicion = 30f;
    public float tamañoTexto = 1f;

    [Header("Velocidad de caída")]
    public float velocidadDeCaida = 5f;

    private float _timerIntervalo;
    private float _timerTotal;
    private float _intervaloActual;
    private bool _activo = false;

    private void Start()
    {
        _activo = true;
        _intervaloActual = Random.Range(intervaloMinSegundos, intervaloMaxSegundos);
    }

    private void Update()
    {
        if (!_activo) return;

        _timerIntervalo += Time.deltaTime;
        _timerTotal += Time.deltaTime;

        if (_timerIntervalo >= _intervaloActual)
        {
            CrearEsferaConNumero();
            _timerIntervalo = 0f;
            _intervaloActual = Random.Range(intervaloMinSegundos, intervaloMaxSegundos);
        }

        if (_timerTotal >= periodoTotalMinutos * 60f)
        {
            _activo = false;
        }
    }

    void CrearEsferaConNumero()
    {
        Vector3 pos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        GameObject esfera = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        esfera.transform.position = pos;
        esfera.name = "EsferaNumerada";

        Collider col = esfera.GetComponent<Collider>();
        col.isTrigger = true;

        Rigidbody rb = esfera.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = Vector3.down * velocidadDeCaida;

        int numero = Random.Range(minNum, maxNum);

        NumeroEnEsfera comp = esfera.AddComponent<NumeroEnEsfera>();
        comp.numero = numero;
        comp.tagsPermitidos = tagsADetectar;
        comp.tiempoDeDesaparicion = tiempoDeDesaparicion;

        // Configuración del TextMesh
        GameObject textoObj = new GameObject("Texto");
        textoObj.transform.SetParent(esfera.transform);
        textoObj.transform.localPosition = Vector3.zero;

        TextMesh texto = textoObj.AddComponent<TextMesh>();
        texto.text = numero.ToString();
        texto.anchor = TextAnchor.MiddleCenter;
        texto.alignment = TextAlignment.Center;
        texto.color = Color.black;
        texto.fontSize = 50; // Usar tamaño compatible
    }
}