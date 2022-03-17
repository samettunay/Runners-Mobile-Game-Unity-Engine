using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject player, panel, heart1, heart2, heart3;
    public PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    /*void Update()
    {
        if(playerMovement.dieCounter == 3)
        reklamButton();
    }*/

    public void reklamButton()
    {
        heart3.SetActive(true);
        heart2.SetActive(true);
        heart1.SetActive(true);
        panel.SetActive(false);
        playerMovement.checkDead = false;
        playerMovement.dieCounter = 0;
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
    }

}
