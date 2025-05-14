// Inicializador.cs
using UnityEngine;
using Atributo;
public class Inicializador : MonoBehaviour
{
    void Start()
    {
        // Asegurar que Atributos se inicialice primero
        Atributos.Instance.InicializarAtributos();
        
        // Opcional: Instanciar el spawner de esferas aquí si es necesario
        // Instantiate(Resources.Load("Prefabs/SpawnerEsferas"));
    }
}