using UnityEngine;
using System.Collections.Generic;

public class NumeroEnEsfera : MonoBehaviour
{
    public int numero;
    public List<string> tagsPermitidos;
    public float tiempoDeDesaparicion;

    private AudioClip sonidoDesaparicion;

    private void Start()
    {
        // Cargar el sonido desde Resources una sola vez
        sonidoDesaparicion = Resources.Load<AudioClip>("Sonidos/desaparecer");

        // Destruir automáticamente si no ha colisionado
        Invoke(nameof(DestruirSinColision), tiempoDeDesaparicion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tagsPermitidos.Contains(other.tag))
        {
            Debug.Log($"[COLISIÓN] '{other.name}' con tag '{other.tag}' tocó esfera {numero}");

            // Enviar el número al GameManager
            GameManager.Instance.PlayerTouchedNumber(other.tag, numero);

            // Reproducir el sonido
            if (sonidoDesaparicion != null)
                AudioSource.PlayClipAtPoint(sonidoDesaparicion, transform.position);

            Destroy(gameObject);
        }
    }

    private void DestruirSinColision()
    {
        Destroy(gameObject);
    }
}
