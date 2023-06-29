using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public Transform player;
    private Rigidbody2D rbEnemy;
    private Vector2 movement;
    private void Start()
    {
        rbEnemy = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rbEnemy.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        rbEnemy.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
