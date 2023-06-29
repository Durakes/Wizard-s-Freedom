using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    [SerializeField] public Transform jugador;
    [SerializeField] private float distancia;

    public Vector3 puntoInicial;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    //Ataque de enemigo 
    public GameObject Player;
    private float LastShoot;
    
    private void Start()
    {        
        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        distancia = Vector2.Distance(transform.position, jugador.position); //distancia entre el jugador y la ara�a
        animator.SetFloat("Distancia", distancia);

        //Instanciar distancia 
        float distanciaDisparo = Mathf.Abs(Player.transform.position.x - transform.position.x);
        //Disparos?
        if(distanciaDisparo < 4f && Time.time > LastShoot + 0.3f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
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
}
