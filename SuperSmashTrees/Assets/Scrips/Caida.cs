using UnityEngine;
using System.Collections;
using texteditor;


public class ReaparecerPorCaida : MonoBehaviour
{
    [Header("Configuración de caída")]
    STModelo GameManager;
    
    public string tagDelObjeto; // Almacena el tag del objeto

    public float alturaMinimaY = -10f;
    public float tiempoDeEspera = 3f;
    public float alturaRespawnY = 2f;
    public float tiempoInmovil = 2f;

    [Header("Rango de respawn")]
    public Vector2 rangoX = new Vector2(-5f, 5f);
    public Vector2 rangoZ = new Vector2(-5f, 5f);

    private bool estaReapareciendo = false;
    private Rigidbody rb;
    private MonoBehaviour controladorMovimiento; // Aquí tu clase "Player" u otra

    void Start()
    {
        GameManager = STModelo.Instance;
        tagDelObjeto = gameObject.tag;
        rb = GetComponent<Rigidbody>();
        controladorMovimiento = GetComponent<Player>(); // Cambia esto si tu clase de movimiento tiene otro nombre
    }

    void Update()
    {
        if (!estaReapareciendo && transform.position.y < alturaMinimaY)
        {
            StartCoroutine(Reaparecer());
        }
    }

    IEnumerator Reaparecer()
    {
        estaReapareciendo = true;

        int num = GameManager.PlayerNum(tagDelObjeto);

        GameManager.PuntosPorCaida(num);
        yield return new WaitForSeconds(tiempoDeEspera);

        // Reubicar al jugador
        float nuevaX = Random.Range(rangoX.x, rangoX.y);
        float nuevaZ = Random.Range(rangoZ.x, rangoZ.y);
        Vector3 nuevaPos = new Vector3(nuevaX, alturaRespawnY, nuevaZ);
        transform.position = nuevaPos;

        // Inmovilizar temporalmente
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (controladorMovimiento != null)
        {
            controladorMovimiento.enabled = false;
        }

        yield return new WaitForSeconds(tiempoInmovil);

        // Restaurar movimiento
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        if (controladorMovimiento != null)
        {
            controladorMovimiento.enabled = true;
        }

        estaReapareciendo = false;
    }
}
