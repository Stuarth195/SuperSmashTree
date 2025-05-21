using UnityEngine;
using System.Collections.Generic;

public class NumeroEnEsfera : MonoBehaviour
{
    public int numero;
    public List<string> tagsPermitidos;
    private string ultimoTagQueToco; // Variable para almacenar el tag

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
        
        ultimoTagQueToco = other.tag; // Guardar el tag en la variable
        
        //GameManager.Instance.PlayerTouchedNumber(ultimoTagQueToco, numero);

        if (sonidoDesaparicion != null)
            AudioSource.PlayClipAtPoint(sonidoDesaparicion, transform.position);

        Destroy(gameObject);
    }
}    private void DestruirSinColision()
    {
        Destroy(gameObject);
    }
}
