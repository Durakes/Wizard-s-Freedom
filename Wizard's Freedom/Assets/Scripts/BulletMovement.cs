using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletMovement : MonoBehaviour
{
    public float damage = 2f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
    }

    void Update()
    {
        Invoke(nameof(DestroyBullet), 3f);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
