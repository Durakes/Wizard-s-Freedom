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

    private void Awake()
    {
        agente = GetComponent <NavMeshAgent>();
    }
    private void Start()
    {
        indiceRuta =0;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }
    private void Update()
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
        
    }

    void MovePersonaje(bool esDetectado)
    {
        if(esDetectado)
        {
            agente.SetDestination(player.position);
        }
        else 
        {
            agente.SetDestination(puntoRuta[indiceRuta].position);
        }
    }
}
