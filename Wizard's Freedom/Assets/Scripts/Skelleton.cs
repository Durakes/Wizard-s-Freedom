using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelleton : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
         
    }
    private void Update()
    {
       
        
        Vector3 direction = Player.transform.position - transform.position;
        if(direction.x >= 0.0f) transform.localScale = new Vector3(5.0f,5.0f,1.0f);
        else transform.localScale = new Vector3(-5.0f,5.0f,1.0f);
    }
}
