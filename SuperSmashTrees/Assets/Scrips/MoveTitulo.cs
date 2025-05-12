using UnityEngine;

public class MovimientoTitulo : MonoBehaviour
{
    public float amplitud = 0.5f;     // Qué tanto se mueve arriba/abajo
    public float velocidad = 1f;      // Qué tan rápido se mueve

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * velocidad) * amplitud;
        transform.position = posicionInicial + new Vector3(0f, offsetY, 0f);
    }
}

