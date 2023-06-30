using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAI : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed, chaseDistance, rangeDistance, meleeDistance;
    private float nextFire;
    private Transform player;
    private Rigidbody2D rbGolem;
    private Vector2 golemMovement;
    private GameObject bullet;
    private GameObject rock;
    public GameObject rockPrefab;
    public GameObject bulletPrefab;
    private int index1 = 0;
    private float timeVulnerable;
    private bool isFury;
    private int lifes;
    private int numberAttacks;
    private enum StateGolem
    {
        Idle,
        Chasing,
        MeleeAttack,
        RangeAttack,
        Fury,
        Vulnerable,
        Death,
    }

    private StateGolem state; 
    // Start is called before the first frame update
    void Start()
    {
        isFury = false;
        lifes = 10;
        numberAttacks = 2;
        player = GameObject.FindWithTag("Player").transform;
        state = StateGolem.Idle;
        rbGolem = this.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(rockPrefab.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        float distancePlayer = Vector2.Distance(this.transform.position, player.position);
        if(!isFury && lifes <= 9)
        {
            Fury();
            isFury = true;
        }

        if(lifes <= 0)
        {
            Destroy(gameObject);
        }

        switch(state)
        {
            case StateGolem.Idle:
                if(distancePlayer <= chaseDistance)
                {
                    state = StateGolem.Chasing;
                }
                break;
            case StateGolem.Chasing:
                if(distancePlayer >= chaseDistance)
                {                    
                    state = StateGolem.Idle;
                }else if(distancePlayer <= rangeDistance)
                {
                    state = StateGolem.RangeAttack;
                }
                golemMovement = PlayerPosition();
                break;
            case StateGolem.RangeAttack:
                if(distancePlayer > rangeDistance)
                {
                    state = StateGolem.Chasing;
                }else if(distancePlayer <= meleeDistance)
                {
                    state = StateGolem.MeleeAttack;
                }
                golemMovement = PlayerPosition();
                break;
            case StateGolem.MeleeAttack:
                if(distancePlayer > meleeDistance && distancePlayer > rangeDistance)
                {
                    state = StateGolem.Chasing;
                }else if(distancePlayer > meleeDistance)
                {
                    state = StateGolem.RangeAttack;
                }
                golemMovement = PlayerPosition();
                break;
            case StateGolem.Vulnerable:
                index1 = 0;
                if(Time.time > timeVulnerable + 2.0f)
                {
                    if(distancePlayer > meleeDistance && distancePlayer > rangeDistance)
                    {
                        state = StateGolem.Chasing;
                    }else if(distancePlayer > meleeDistance)
                    {
                        state = StateGolem.RangeAttack;
                    }else if(distancePlayer <= meleeDistance)
                    {
                        state = StateGolem.MeleeAttack;
                    }
                    golemMovement = PlayerPosition();
                }
                break;
            default:
                state = StateGolem.Idle;
                break;
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case StateGolem.Chasing:
                rbGolem.MovePosition((Vector2)transform.position + (golemMovement * moveSpeed * Time.fixedDeltaTime));
                break;
            case StateGolem.RangeAttack:
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + 0.7f;
                    ThrowRock();
                    index1++;
                }
                if(index1 >= numberAttacks)
                {
                    timeVulnerable = Time.time;
                    state = StateGolem.Vulnerable;
                }
                break;
            default:
                break;
        }
    }

    Vector3 PlayerPosition()
    {
        Vector3 directionShoot = player.position - transform.position;
        directionShoot.Normalize();
        return directionShoot;
    }
    void ThrowRock()
    {
        rock = Instantiate(rockPrefab, transform.position + ((Vector3)golemMovement * 1.5f),  Quaternion.identity);
        rock.GetComponent<Rigidbody2D>().AddForce(golemMovement * 10f, ForceMode2D.Impulse);
    }

    void Fury()
    {
        moveSpeed = 5f;
        chaseDistance = 15f;
        numberAttacks = 5;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        lifes--;    
    }
}
