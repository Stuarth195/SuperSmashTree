using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float tiempoDeVida = 2f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }
}
