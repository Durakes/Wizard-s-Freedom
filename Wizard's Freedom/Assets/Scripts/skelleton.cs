using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skelleton : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float distancia; 
    public Vector3 puntoInicial;
    private Animator animator;
    private SpriteRenderer spriterenderer;
    private int lifes;


    // Start is called before the first frame update
    private void Start()
    {
        lifes = 5;
        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
        spriterenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        distancia = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("Distancia", distancia);

        if(lifes <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    public void Girar(Vector3 objetivo)
    {
        if(transform.position.x < objetivo.x)
        {
            transform.localScale = new Vector3(5,5);
            //spriterenderer.flipX = true; //Si la posicion que tiene actualmente es menor que el objetivo, invierte la posicion
        }
        else
        {
            transform.localScale = new Vector3(-5,5);
            //spriterenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.gameObject.CompareTag("Player"))
        {
        animator.SetTrigger("Ataca");
         }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            lifes--;
        }
    }
    
}
