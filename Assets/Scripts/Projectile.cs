using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log($"OnTriggerEnter2D {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
            // Destroy(this.gameObject);
        }
    }
}
