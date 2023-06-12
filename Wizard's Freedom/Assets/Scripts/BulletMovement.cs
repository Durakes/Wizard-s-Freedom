using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float damage = 2f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
