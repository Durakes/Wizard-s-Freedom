using UnityEditorInternal;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private float chaseDistance, rangeDistance;
    private float nextFire;
    public float moveSpeed = 1.5f;
    public Transform player;
    private Rigidbody2D rbBoss;
    private Vector2 movement;
    private GameObject bulletBoss;
    public GameObject bulletBossPrefab;
    private StateBoss state;
    private float timeAttacking;
    private int lifes;
    private float timeVulnerable;
    private float angle = 0f;
    private enum StateBoss
    {
        Idle,
        Chasing,
        FollowAttack,
        CircularAttack,
        FanAttack,
        Vulnerable,
        Death,
    }


    private void Start()
    {
        lifes = 20;
        timeAttacking = 5;
        rbBoss = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        state = StateBoss.Idle;
        Physics2D.IgnoreCollision(bulletBossPrefab.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(bulletBossPrefab.GetComponent<Collider2D>(), bulletBossPrefab.GetComponent<Collider2D>());
    }

    private void Update()
    {
        float distancePlayer = Vector2.Distance(this.transform.position, player.position);
        if(lifes <= 0)
        {
            Destroy(gameObject);
        }
        
        switch (state)
        {
            case StateBoss.Idle:
                if(distancePlayer <= chaseDistance)
                {
                    state = StateBoss.Chasing;
                }
                break;
            case StateBoss.Chasing:
                if(distancePlayer >= chaseDistance)
                {                    
                    state = StateBoss.Idle;
                }else if(distancePlayer <= rangeDistance)
                {
                    state = StateBoss.FollowAttack;
                }
                movement = PlayerPosition();
                break;
            case StateBoss.FollowAttack:
                movement = PlayerPosition();
                break;
            case StateBoss.FanAttack:
                movement = PlayerPosition();
                break;
            case StateBoss.Vulnerable: //Implementar
                Debug.Log("Waiting");
                if(Time.time > timeVulnerable + 2.0f)
                {
                    if(distancePlayer <= chaseDistance)
                    {
                        state = StateBoss.Chasing;
                    }
                    movement = PlayerPosition();
                }
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case StateBoss.Chasing:
                moveCharacter(movement);
                break;
            case StateBoss.FollowAttack:
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + 0.7f;
                    FollowRangeAttack();
                }
                timeAttacking -= Time.deltaTime;
                if(timeAttacking <= 0)
                {
                    timeAttacking = 10f;
                    state = StateBoss.CircularAttack;
                }
                break;
            case StateBoss.CircularAttack:
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + 0.15f;
                    CircularAttack();
                }
                timeAttacking -= Time.deltaTime;
                if(timeAttacking <= 0)
                {
                    timeAttacking = 10f;
                    state = StateBoss.FanAttack;
                }
                break;
            case StateBoss.FanAttack:
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + 0.5f;
                    FanAttack();
                }
                timeAttacking -= Time.deltaTime;
                if(timeAttacking <= 0)
                {
                    timeVulnerable = Time.time;
                    state = StateBoss.Vulnerable;
                }
                break;
            default:
                break;
        }
    }
    private void moveCharacter(Vector2 direction)
    {
        rbBoss.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    Vector3 PlayerPosition()
    {
        Vector3 directionShoot = player.position - transform.position;
        directionShoot.Normalize();
        return directionShoot;
    }

    void FollowRangeAttack()
    {
        bulletBoss = Instantiate(bulletBossPrefab, transform.position + ((Vector3)movement * 1.3f),  Quaternion.identity); //1.3f cambiar valor
        Physics2D.IgnoreCollision(bulletBoss.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        bulletBoss.GetComponent<Rigidbody2D>().AddForce(movement * 7f, ForceMode2D.Impulse); //7f cambiar valor
    }

    void CircularAttack()
    {
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
        
        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;
        
        bulletBoss = Instantiate(bulletBossPrefab, transform.position + ((Vector3)bulDir * 1.3f), Quaternion.identity);
        Physics2D.IgnoreCollision(bulletBoss.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        bulletBoss.GetComponent<Rigidbody2D>().AddForce(bulDir * 7f, ForceMode2D.Impulse);

        angle -= 10f;
    }

    void FanAttack()
    {
        // Bala 1
        float bulDirX = Mathf.Cos((10 * Mathf.PI) / 180f) * movement.x - Mathf.Sin((10 * Mathf.PI) / 180f) * movement.y;
        float bulDirY = Mathf.Sin((10 * Mathf.PI) / 180f) * movement.x + Mathf.Cos((10 * Mathf.PI) / 180f) * movement.y;
        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector).normalized;

        // Bala 2
        float bulDirX2 = Mathf.Cos((350 * Mathf.PI) / 180f) * movement.x - Mathf.Sin((345 * Mathf.PI) / 180f) * movement.y;
        float bulDirY2 = Mathf.Sin((350 * Mathf.PI) / 180f) * movement.x + Mathf.Cos((345 * Mathf.PI) / 180f) * movement.y;
        Vector3 bulMoveVector2 = new Vector3(bulDirX2, bulDirY2, 0f);
        Vector2 bulDir2 = (bulMoveVector2).normalized;

        bulletBoss = Instantiate(bulletBossPrefab, transform.position + ((Vector3)movement * 1.3f),  Quaternion.identity); //1.3f cambiar valor
        bulletBoss.GetComponent<Rigidbody2D>().AddForce(movement * 7f, ForceMode2D.Impulse); //7f cambiar valor
        GameObject bullet2 = Instantiate(bulletBossPrefab, transform.position + ((Vector3)bulDir * 1.3f),  Quaternion.identity); //1.3f cambiar valor
        bullet2.GetComponent<Rigidbody2D>().AddForce(bulDir * 7f, ForceMode2D.Impulse);
        GameObject bullet3 = Instantiate(bulletBossPrefab, transform.position + ((Vector3)bulDir2 * 1.3f),  Quaternion.identity); //1.3f cambiar valor
        bullet3.GetComponent<Rigidbody2D>().AddForce(bulDir2 * 7f, ForceMode2D.Impulse); //7f cambiar valor*/
        Physics2D.IgnoreCollision(bulletBoss.GetComponent<Collider2D>(), bullet2.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(bulletBoss.GetComponent<Collider2D>(), bullet3.GetComponent<Collider2D>()); //7f cambiar valor
        Physics2D.IgnoreCollision(bullet2.GetComponent<Collider2D>(), bullet3.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(bulletBoss.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(bullet2.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(bullet3.GetComponent<Collider2D>(), this.GetComponent<Collider2D>()); //7f cambiar valor
        Debug.Log("Fan");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            lifes--;
        }

        rbBoss.inertia = 0f;   
        rbBoss.velocity = Vector2.zero;
        rbBoss.angularVelocity = 0f;
    }
}
