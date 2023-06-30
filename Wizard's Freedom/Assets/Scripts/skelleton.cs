using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skelleton : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private float distancia; 
    public Vector3 puntoInicial;
    private Animator animator;
    private SpriteRenderer spriterenderer;



    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        distancia = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("Distancia", distancia);

    }
    public void Girar(Vector3 objetivo)
    {
        if(transform.position.x < objetivo.x)
        {
            spriterenderer.flipX = true; //Si la posicion que tiene actualmente es menor que el objetivo, invierte la posicion
        }
        else
        {
            spriterenderer.flipX = false;
        }
    }
}
