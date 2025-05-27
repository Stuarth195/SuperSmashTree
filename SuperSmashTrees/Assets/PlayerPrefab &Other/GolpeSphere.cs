using UnityEngine;

public class Golpe : MonoBehaviour
{
    public float velocidad = 10f;
    public float fuerzaEmpuje = 1000f;
    public float duracion = 0.5f;

    private Vector3 direccionDeMovimiento;

    public void SetDireccion(Vector3 direccion)
    {
        direccionDeMovimiento = direccion.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, duracion);
    }

    private void Update()
    {
        transform.position += direccionDeMovimiento * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
{
    // Si choca con escudo, se destruye sin empujar nada
    if (other.CompareTag("Escudo"))
    {
        Debug.Log("🛡️ El golpe fue bloqueado por un escudo");
        Destroy(gameObject);
        return;
    }

    // Si choca con un jugador, lo empuja
    if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3"))
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null && !rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(direccionDeMovimiento * fuerzaEmpuje, ForceMode.VelocityChange);

            Debug.Log($"💥 Golpe empujó recto a {other.gameObject.name}");
        }
    }
}

}
