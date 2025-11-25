using UnityEngine;

public class MovingPlatform2 : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // When the player lands on the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlatformerController player = collision.collider.GetComponent<PlatformerController>();
        if (player != null)
        {
            player.SetParent(transform); // 👈 attach player to platform
        }
    }

    // When the player leaves the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlatformerController player = collision.collider.GetComponent<PlatformerController>();
        if (player != null)
        {
            player.ResetParent(); // 👈 detach player
        }
    }
}
