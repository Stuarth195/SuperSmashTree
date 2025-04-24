using UnityEngine;

public class SpawnPlayerArmature : MonoBehaviour
{
    // Nombre del prefab en la carpeta Resources
    private const string prefabName = "PlayerArmature";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject prefab = Resources.Load<GameObject>(prefabName);

            if (prefab != null)
            {
                // Instancia en la posición (0, 0, 0) sin rotación
                Instantiate(prefab, Vector3.zero, Quaternion.identity);
                Debug.Log("PlayerArmature instanciado.");
            }
            else
            {
                Debug.LogError($"No se encontró el prefab '{prefabName}' en la carpeta Resources.");
            }
        }
    }
}
