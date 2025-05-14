using UnityEngine;

public class CrearObjetoConTecla : MonoBehaviour
{
    [Header("Objeto a instanciar")]
    [SerializeField] private GameObject prefabAInstanciar;

    [Header("Posición de aparición (opcional)")]
    [SerializeField] private Vector3 posicionInstancia = Vector3.zero;
    [SerializeField] private Quaternion rotacionInstancia = Quaternion.identity;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (prefabAInstanciar != null)
            {
                Instantiate(prefabAInstanciar, posicionInstancia, rotacionInstancia);
                Debug.Log("Objeto instanciado al presionar P.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado un prefab para instanciar.");
            }
        }
    }
}
    