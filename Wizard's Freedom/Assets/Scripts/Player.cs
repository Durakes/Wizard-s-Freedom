using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] UIMaganer uiManager;
    private int lifes = 3;
    //private bool isAlive = true;
    private Animator anim;
    void Awake()
    {
       anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
       PlayerDeath();
        }
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
            uiManager.RestaCorazones(lifes);
            if(lifes == 0)
            {
                anim.SetTrigger("muerte");
                Invoke(nameof(Muerte),1f);
            }
    }
    }
    private void Muerte()
    {
        Destroy(this.gameObject);
    }
}
