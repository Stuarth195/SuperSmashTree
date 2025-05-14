using UnityEngine;
using System.Collections.Generic;

public class NumeroEnEsfera : MonoBehaviour
{
    public int numero;
    public List<string> tagsPermitidos;
    private string ultimoTagQueToco;
    public float tiempoDeDesaparicion;
    private AudioClip sonidoDesaparicion;

    private void Start()
    {
        sonidoDesaparicion = Resources.Load<AudioClip>("Sonidos/desaparecer");
        Invoke(nameof(DestruirSinColision), tiempoDeDesaparicion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tagsPermitidos.Contains(other.tag))
        {
            Debug.Log($"[COLISIÓN] '{other.name}' con tag '{other.tag}' tocó esfera {numero}");
            ultimoTagQueToco = other.tag;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerTouchedNumber(ultimoTagQueToco, numero);
            }
            else
            {
                Debug.LogError("GameManager.Instance es null");
            }

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