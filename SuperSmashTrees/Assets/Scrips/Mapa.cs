using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Prefab de la Plataforma")]
    public GameObject plataformaPrefab;

    [Header("Cantidad de Plataformas")]
    public int minPlataformas = 1;
    public int maxPlataformas = 4;

    [Header("Rango de Posición")]
    public Vector3 minPosicion = new Vector3(-10f, 0f, -10f);
    public Vector3 maxPosicion = new Vector3(10f, 0f, 10f);

    [Header("Rango de Rotación")]
    public Vector3 minRotacion = new Vector3(0f, 0f, 0f);
    public Vector3 maxRotacion = new Vector3(0f, 360f, 0f);

    [Header("Rango de Escala (Tamaño)")]
    public Vector3 minEscala = new Vector3(1f, 1f, 1f);
    public Vector3 maxEscala = new Vector3(5f, 5f, 5f);

    [Header("Carpeta de Texturas")]
    public string carpetaTexturas = "Texturas";  // Nombre de la carpeta en "Resources"

    private GameObject[] plataformasExistentes;

    private void Start()
    {
        CrearPlataformas();
    }

    private void CrearPlataformas()
    {
        int cantidad = UnityEngine.Random.Range(minPlataformas, maxPlataformas + 1);
        Debug.Log($"Creando {cantidad} plataformas...");

        plataformasExistentes = new GameObject[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            bool posicionValida = false;
            Vector3 posicion = Vector3.zero;

            // Buscar una posición válida
            while (!posicionValida)
            {
                posicion = new Vector3(
                    UnityEngine.Random.Range(minPosicion.x, maxPosicion.x),
                    UnityEngine.Random.Range(minPosicion.y, maxPosicion.y),
                    UnityEngine.Random.Range(minPosicion.z, maxPosicion.z)
                );

                posicionValida = EsPosicionValida(posicion);
            }

            Vector3 rotacion = new Vector3(
                UnityEngine.Random.Range(minRotacion.x, maxRotacion.x),
                UnityEngine.Random.Range(minRotacion.y, maxRotacion.y),
                UnityEngine.Random.Range(minRotacion.z, maxRotacion.z)
            );

            Vector3 escala = new Vector3(
                UnityEngine.Random.Range(minEscala.x, maxEscala.x),
                UnityEngine.Random.Range(minEscala.y, maxEscala.y),
                UnityEngine.Random.Range(minEscala.z, maxEscala.z)
            );

            GameObject plataforma = Instantiate(plataformaPrefab, posicion, Quaternion.Euler(rotacion));
            plataforma.transform.localScale = escala;

            // Asignar una textura aleatoria
            AsignarTexturaAleatoria(plataforma);

            // Almacenar la plataforma creada
            plataformasExistentes[i] = plataforma;
        }
    }

    // Método para verificar que no haya otra plataforma cerca (menos de 5 unidades)
    private bool EsPosicionValida(Vector3 nuevaPosicion)
    {
        foreach (GameObject plataforma in plataformasExistentes)
        {
            if (plataforma != null && Vector3.Distance(plataforma.transform.position, nuevaPosicion) < 5f)
            {
                return false;  // Está demasiado cerca de otra plataforma
            }
        }
        return true;  // Posición válida
    }

    // Método para asignar una textura aleatoria de la carpeta especificada
    private void AsignarTexturaAleatoria(GameObject plataforma)
    {
        // Cargar todas las texturas de la carpeta de recursos
        Texture[] texturas = Resources.LoadAll<Texture>(carpetaTexturas);

        if (texturas.Length > 0)
        {
            // Seleccionar una textura aleatoria
            Texture texturaAleatoria = texturas[UnityEngine.Random.Range(0, texturas.Length)];

            // Asignar la textura al material de la plataforma
            plataforma.GetComponent<Renderer>().material.mainTexture = texturaAleatoria;
        }
        else
        {
            Debug.LogWarning("No se encontraron texturas en la carpeta especificada.");
        }
    }
}
