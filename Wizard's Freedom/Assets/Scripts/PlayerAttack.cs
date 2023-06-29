using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;
    public PlayerMovement playerMovement;
    [SerializeField]private float speedFire = 10f;
    [SerializeField]private float nextFire;
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + 0.3f;
            ShootingBullet();
        }
    }

    void CreatePlayerBullet()
    {
        Vector3 positionPlayer = playerMovement.moveInput;
        Vector3 positionPlayerIdle = playerMovement.lastMove;
        if(positionPlayer != Vector3.zero)
            bullet = Instantiate(bulletPrefab, transform.position + (positionPlayer * 0.7f),  Quaternion.identity);
        else
            bullet = Instantiate(bulletPrefab, transform.position +  (positionPlayerIdle * 0.7f),  Quaternion.identity);
    }

    void ShootingBullet()
    {
        if(playerMovement.moveInput != Vector2.zero)
        {
            CreatePlayerBullet();
            bullet.GetComponent<Rigidbody2D>().AddForce(playerMovement.moveInput * speedFire, ForceMode2D.Impulse);
        }else
        { 
            CreatePlayerBullet();
            bullet.GetComponent<Rigidbody2D>().AddForce(playerMovement.lastMove * speedFire, ForceMode2D.Impulse);
        }
    }
}
