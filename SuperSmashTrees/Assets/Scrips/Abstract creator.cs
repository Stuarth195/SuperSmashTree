using UnityEngine;

/// <summary>
/// Clase abstracta que funciona como fábrica de objetos en Unity.
/// Hereda de MonoBehaviour para permitir su uso en GameObjects.
/// </summary>
public abstract class ObjectFactory : MonoBehaviour
{
    #region Configuration
    [Tooltip("Prefab base del objeto a instanciar. Asignar desde el Editor.")]
    [SerializeField] protected GameObject objectPrefab;

    [Tooltip("Tag que se asignará al objeto creado. Asegúrate de que exista en Project Settings > Tags and Layers.")]
    [SerializeField] protected string objectTag = "Default";
    #endregion

    #region Object Creation
    /// <summary>
    /// Crea un objeto en la escena con el prefab, posición y tag configurados.
    /// </summary>
    /// <param name="position">Posición en el mundo donde aparecerá el objeto.</param>
    /// <returns>El GameObject creado, o null si hay un error.</returns>
    protected GameObject CreateObject(Vector3 position)
    {
        // Validación básica
        if (objectPrefab == null)
        {
            Debug.LogError("Error en ObjectFactory: Prefab no asignado en el Inspector.", this);
            return null;
        }

        // Instanciar el objeto
        GameObject newObject = Instantiate(objectPrefab, position, Quaternion.identity, transform); // Opcional: lo hace hijo de este GameObject

        // Asignar el tag
        if (!string.IsNullOrEmpty(objectTag))
        {
            newObject.tag = objectTag;
        }
        else
        {
            Debug.LogWarning("Tag no asignado en ObjectFactory. Usando tag por defecto.", this);
        }

        // Configuración adicional (para sobrescribir en clases hijas)
        ConfigureObject(newObject);

        return newObject;
    }
    #endregion

    #region Customization
    /// <summary>
    /// Método virtual para personalizar el objeto después de crearlo.
    /// Sobrescribe este método en clases hijas para añadir lógica específica.
    /// </summary>
    /// <param name="obj">El GameObject recién creado.</param>
    protected virtual void ConfigureObject(GameObject obj)
    {
        // Por defecto: Vacío. ¡Sobrescribe según necesites!
    }
    #endregion
}
