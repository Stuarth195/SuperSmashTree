using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    public string nombreCancion = "musica"; // Nombre del archivo sin la extensión (.mp3)

    private void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + nombreCancion);

        if (clip != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.playOnAwake = true;
            audioSource.volume = 0.5f; // Puedes ajustar el volumen
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se encontró el audio: " + nombreCancion);
        }
    }
}
