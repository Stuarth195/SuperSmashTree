using UnityEngine;

public class SpawnPrefabOnKey : MonoBehaviour
{
    [Tooltip("Letra para instanciar el prefab (solo una letra, sin mayúsculas)")]
    public string letra = "a";

    [Tooltip("Prefab a instanciar")]
    public GameObject prefab;

    private bool yaInstanciado = false;

    void Update()
    {
        if (!yaInstanciado && !string.IsNullOrEmpty(letra) && letra.Length == 1 && prefab != null)
        {
            if (Input.GetKeyDown(letra.ToLower()))
            {
                Instantiate(prefab, Vector3.zero, Quaternion.identity);
                yaInstanciado = true;
            }
        }
    }
}
