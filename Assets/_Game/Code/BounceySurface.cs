using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField] private float bounceForce = 10f; // 👈 editable in Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}

