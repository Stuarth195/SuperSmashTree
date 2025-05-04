using UnityEngine;

public class ReproductorAleatorio : MonoBehaviour
{
    [Range(0f, 1f)]
    public float volumen = 0.5f; // Control de volumen desde el Inspector

    private AudioSource audioSource;
    private AudioClip[] canciones;

    void Start()
    {
        canciones = Resources.LoadAll<AudioClip>("Audio");

        if (canciones.Length == 0)
        {
            Debug.LogWarning("No se encontraron canciones en Resources/Audio.");
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.volume = volumen;

        ReproducirCancionAleatoria();
    }

    void Update()
    {
        // Si la canción terminó, reproduce otra
        if (!audioSource.isPlaying && canciones.Length > 0)
        {
            ReproducirCancionAleatoria();
        }

        // Permite cambiar el volumen en tiempo real desde el Inspector
        audioSource.volume = volumen;
    }

    void ReproducirCancionAleatoria()
    {
        int indice = Random.Range(0, canciones.Length);
        audioSource.clip = canciones[indice];
        audioSource.Play();
        Debug.Log("🎵 Reproduciendo: " + canciones[indice].name);
    }
}
