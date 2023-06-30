using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] UIMaganer uiManager;
    private int lifes;
    public bool isAlive = true;
    private Animator anim;
    void Awake()
    {
        lifes = 5;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (lifes == 0)
        {
            PlayerDeath();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Si se trabaja con Capas, o con tags o simplemente con el nombre
        if (collision.gameObject.CompareTag("EnemyBullet")) 
        {
            PlayerDeath();
        }
        /*else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            // Sirve para poner el powerUp si es que se realiza
        }*/
    }

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
                isAlive = false;
            }
    }
    }
    private void Muerte()
    {
        this.gameObject.SetActive(false);
    }
}
