using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Prefab de la Plataforma")]
    public GameObject plataformaPrefab;

    [Header("Cantidad de Plataformas")]
    public int minPlataformas = 1;
    public int maxPlataformas = 4;

    [Header("Rango de Posición (X, Y, Z)")]
    public Vector3 minPosicion = new Vector3(-10f, 0f, -10f);
    public Vector3 maxPosicion = new Vector3(10f, 0f, 10f);

    [Header("Offset entre Plataformas (X ±, Y ±)")]
    public float offsetX = 10f;
    public float offsetY = 2f;

    [Header("Distancia mínima entre plataformas")]
    public float distanciaMinima = 5f;

    [Header("Rango de Rotación")]
    public Vector3 minRotacion = new Vector3(0f, 0f, -20f);
    public Vector3 maxRotacion = new Vector3(0f, 360f, 20f);

    [Header("Rango de Escala")]
    public Vector3 minEscala = new Vector3(1f, 1f, 1f);
    public Vector3 maxEscala = new Vector3(5f, 5f, 5f);

    [Header("Carpeta de Materiales (Resources)")]
    public string carpetaMateriales = "Materiales";

    private GameObject[] plataformasExistentes;

    private void Start()
    {
        CrearPlataformas();
    }

    private void CrearPlataformas()
    {
        int cantidad = Random.Range(minPlataformas, maxPlataformas + 1);
        Debug.Log($"Creando {cantidad} plataformas...");

        plataformasExistentes = new GameObject[cantidad];

        Vector3 ultimaPosicion = Vector3.zero;
        bool posicionValida = false;
        int intentos = 0;
        int maxIntentos = 1000;

        // Primera plataforma
        while (!posicionValida && intentos < maxIntentos)
        {
            ultimaPosicion = new Vector3(
                Random.Range(minPosicion.x, maxPosicion.x),
                Random.Range(minPosicion.y, maxPosicion.y),
                Random.Range(minPosicion.z, maxPosicion.z)
            );

            posicionValida = EsPosicionValida(ultimaPosicion);
            intentos++;
        }

        if (!posicionValida)
        {
            Debug.LogWarning("No se pudo encontrar una posición válida para la primera plataforma.");
            return;
        }

        GameObject primeraPlataforma = CrearPlataforma(ultimaPosicion);
        plataformasExistentes[0] = primeraPlataforma;

        // Siguientes plataformas
        for (int i = 1; i < cantidad; i++)
        {
            posicionValida = false;
            intentos = 0;
            Vector3 nuevaPosicion = Vector3.zero;

            while (!posicionValida && intentos < maxIntentos)
            {
                float deltaX = Random.Range(-offsetX, offsetX);
                float deltaY = Random.Range(-offsetY, offsetY);
                float nuevaZ = Random.Range(minPosicion.z, maxPosicion.z);

                nuevaPosicion = new Vector3(
                    Mathf.Clamp(ultimaPosicion.x + deltaX, minPosicion.x, maxPosicion.x),
                    Mathf.Clamp(ultimaPosicion.y + deltaY, minPosicion.y, maxPosicion.y),
                    nuevaZ
                );

                posicionValida = EsPosicionValida(nuevaPosicion);
                intentos++;
            }

            if (!posicionValida)
            {
                Debug.LogWarning($"No se pudo encontrar una posición válida para la plataforma {i + 1}. Se omitirá.");
                continue;
            }

            GameObject nuevaPlataforma = CrearPlataforma(nuevaPosicion);
            plataformasExistentes[i] = nuevaPlataforma;
            ultimaPosicion = nuevaPosicion;
        }
    }

    private GameObject CrearPlataforma(Vector3 posicion)
    {
        Vector3 rotacion = new Vector3(
            Random.Range(minRotacion.x, maxRotacion.x),
            Random.Range(minRotacion.y, maxRotacion.y),
            Random.Range(minRotacion.z, maxRotacion.z)
        );

        Vector3 escala = new Vector3(
            Random.Range(minEscala.x, maxEscala.x),
            Random.Range(minEscala.y, maxEscala.y),
            Random.Range(minEscala.z, maxEscala.z)
        );

        GameObject plataforma = Instantiate(plataformaPrefab, posicion, Quaternion.Euler(rotacion));
        plataforma.transform.localScale = escala;
        AsignarMaterialAleatorio(plataforma);
        return plataforma;
    }

    private bool EsPosicionValida(Vector3 nuevaPosicion)
    {
        foreach (GameObject plataforma in plataformasExistentes)
        {
            if (plataforma != null && Vector3.Distance(plataforma.transform.position, nuevaPosicion) < distanciaMinima)
            {
                return false;
            }
        }
        return true;
    }

    private void AsignarMaterialAleatorio(GameObject plataforma)
    {
        Material[] materiales = Resources.LoadAll<Material>(carpetaMateriales);

        if (materiales.Length > 0)
        {
            Material materialAleatorio = materiales[Random.Range(0, materiales.Length)];
            Renderer renderer = plataforma.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material = materialAleatorio;
            }
        }
        else
        {
            Debug.LogWarning("No se encontraron materiales en la carpeta especificada.");
        }
    }
}
