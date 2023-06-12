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
        Debug.Log(playerMovement.moveInput);
        if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + 0.3f;
            ShootingBullet();
        }
    }

    void CreatePlayerBullet()
    {
        Vector3 positionPlayer = playerMovement.moveInput;
        if(positionPlayer != Vector3.zero)
            bullet = Instantiate(bulletPrefab, transform.position + positionPlayer,  Quaternion.identity);
        else
            bullet = Instantiate(bulletPrefab, transform.position + Vector3.down,  Quaternion.identity);
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
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speedFire, ForceMode2D.Impulse);
        }
    }
}
