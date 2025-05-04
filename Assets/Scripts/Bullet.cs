using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Rock"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
        Destroy(gameObject, 3f);
    }
}