using UnityEngine;

public class RigidBodyPush : MonoBehaviour
{
    public LayerMask pushLayers;
    public bool canPush = true;
    [Range(0.5f, 5f)] public float strength = 1.1f;

    void Start()
    {
        
    }

    void Update()
    {
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush) PushRigidBodies(hit);
    }

    private void PushRigidBodies(ControllerColliderHit hit)
    {
        // https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

        // make sure we hit a non kinematic rigidbody
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        // New: Only push if object passes filter
        if (!CanPushObject(hit.collider.gameObject)) return;

        // make sure we only push desired layer(s)
        var bodyLayerMask = 1 << body.gameObject.layer;
        if ((bodyLayerMask & pushLayers.value) == 0) return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f) return;

        // Calculate push direction from move direction, horizontal motion only
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

        // Apply the push and take strength into account
        body.AddForce(pushDir * strength, ForceMode.Impulse);
    }

    // New method: filter objects to push only same prefab or tagged Player1, Player2, Player3
    private bool CanPushObject(GameObject obj)
    {
        // Prevent pushing itself
        if (obj == this.gameObject) return false;

        // Check if tag is one of player1, player2 or player3
        string tag = obj.tag;
        return tag == "Player1" || tag == "Player2" || tag == "Player3";
    }
}
