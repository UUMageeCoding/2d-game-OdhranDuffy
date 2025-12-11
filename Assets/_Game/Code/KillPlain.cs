using UnityEngine;

public class KillZone : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;   // Drag your respawn point here in Inspector
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (respawnPoint == null) return;

        // Detach from any moving platform
        other.transform.SetParent(null);

        // Teleport to respawn point
        other.transform.position = respawnPoint.position;

        // Reset physics so player doesn’t keep falling/sliding
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
