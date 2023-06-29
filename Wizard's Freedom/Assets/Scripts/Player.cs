using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private int lifes;
    private bool isAlive = true;
    void Awake()
    {
        lifes = 3;
    }
    void Update()
    {
       PlayerDeath();
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        //Si se trabaja con Capas, o con tags o simplemente con el nombre
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {
            lifes--;
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            // Sirve para poner el powerUp si es que se realiza
        }
    }*/

    public void PlayerDeath()
    {
         if(lifes >= 0)
        {
            lifes--;
            //PlayerDeath();
            //isAlive = false;
            if (lifes == 0)
            {
                //Debug.Log("PIPIPIIP");
            }
        }
    }
}
