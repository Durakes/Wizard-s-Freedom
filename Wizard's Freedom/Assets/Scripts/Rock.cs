using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Vector2 playerPosition;

    private void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
    }
    // Start is called before the first frame update\

    private void Update()
    {
        if((Vector2.Distance(this.transform.position, playerPosition) < 0.3f))
            ExploteRock();
    }

    public void ExploteRock()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);   
    }
}
