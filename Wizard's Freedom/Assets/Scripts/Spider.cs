using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    [SerializeField] private float distancia;
    private Transform player;
    public Vector3 puntoInicial;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    //Ataque de enemigo 
    private float LastShoot;
    private int lifes;
    public GameObject bulletPrefab;
    private GameObject bullet;
    Vector2 spiderDirection;
    private void Start()
    {
        lifes = 3;
        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        distancia = Vector2.Distance(transform.position, player.position); //distancia entre el jugador y la ara�a
        animator.SetFloat("Distancia", distancia);

        spiderDirection = PlayerPosition();

        //Instanciar distancia 
        float distanciaDisparo = Mathf.Abs(player.position.x - transform.position.x);
        //Disparos?
        if(distanciaDisparo < 4f && Time.time > LastShoot + 0.7f)
        {
            LastShoot = Time.time;
            Shoot();
        }

        if(lifes <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Shoot()
    {
        bullet = Instantiate(bulletPrefab, transform.position + ((Vector3)spiderDirection * 1.3f),  Quaternion.identity); //1.3f cambiar valor
        bullet.GetComponent<Rigidbody2D>().AddForce(spiderDirection * 7f, ForceMode2D.Impulse);
    }

    public void Girar(Vector3 objetivo)
    {
        if(transform.position.x < objetivo.x)
        {
            spriteRenderer.flipX = true; //Si la posicion que tiene actualmente es menor que el objetivo, invierte la posicion
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            lifes--;
        }
    } 

    Vector3 PlayerPosition()
    {
        Vector3 directionShoot = player.position - transform.position;
        directionShoot.Normalize();
        return directionShoot;
    }
}
