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

    [Header("Carpeta de Materiales")]
    public string carpetaMateriales = "Materiales";  // Carpeta dentro de Resources

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

        for (int i = 0; i < cantidad; i++)
        {
            bool posicionValida = false;
            Vector3 posicion = Vector3.zero;
            int intentos = 0;
            int maxIntentos = 100;

            while (!posicionValida && intentos < maxIntentos)
            {
                posicion = new Vector3(
                    Random.Range(minPosicion.x, maxPosicion.x),
                    Random.Range(minPosicion.y, maxPosicion.y),
                    Random.Range(minPosicion.z, maxPosicion.z)
                );

                posicionValida = EsPosicionValida(posicion);
                intentos++;
            }

            if (!posicionValida)
            {
                Debug.LogWarning($"No se pudo encontrar una posición válida para la plataforma {i + 1}. Se omitirá.");
                continue;
            }

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

            // Asignar un material aleatorio
            AsignarMaterialAleatorio(plataforma);

            plataformasExistentes[i] = plataforma;
        }
    }

    private bool EsPosicionValida(Vector3 nuevaPosicion)
    {
        foreach (GameObject plataforma in plataformasExistentes)
        {
            if (plataforma != null && Vector3.Distance(plataforma.transform.position, nuevaPosicion) < 20f)
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
