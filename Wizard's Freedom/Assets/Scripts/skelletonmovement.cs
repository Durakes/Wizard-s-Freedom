using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class skelletonmovement : MonoBehaviour
{
    public Transform player;
    public Transform[] puntoRuta;
    private int indiceRuta;
    private NavMeshAgent agente;
    private bool personajeDetectado;
    private SpriteRenderer sprite;
    private Transform objetivo;
    private Animator anm;
    //private Animator anmattack;

    private void Awake()
    {
        agente = GetComponent <NavMeshAgent>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anm = GetComponentInChildren<Animator>();
        //anmattack = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        indiceRuta =0;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }
    private void FixedUpdate()
    {

        
        this.transform.position = new Vector3(transform.position.x,transform.position.y,0);
        float distancia = Vector3.Distance(player.position, this.transform.position);
        if (this.transform.position == puntoRuta[indiceRuta].position)
        {
            if(indiceRuta < puntoRuta.Length -1)
            {
                indiceRuta++;
            }
            else if(indiceRuta == puntoRuta.Length -1)
            {
                indiceRuta = 0;
            }
        
        }
        if(distancia<6)
        {
            personajeDetectado = true;
        }

        MovePersonaje(personajeDetectado);
        Rotar();
    }

    void MovePersonaje(bool esDetectado)
    {
        if(esDetectado)
        {
            agente.SetDestination(player.position);
            objetivo = player;
            anm.SetFloat ("run",Mathf.Abs (1));
        }
        else 
        {
            agente.SetDestination(puntoRuta[indiceRuta].position);
            objetivo = puntoRuta[indiceRuta];
        }
    }
    void Rotar()
    {
        if(this.transform.position.x > objetivo.position.x)
        {
            sprite.flipX=true;
        }
        else 
        {
            sprite.flipX =false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Player"))
         {
            anm.SetTrigger("attack");
         }
    }
}
