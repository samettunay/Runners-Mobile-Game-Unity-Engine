using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave3 : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private bool playerCame = false;
    private float timer = 0.35f;
    public GameObject rocket;
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && playerCame)
        {
            playerMovement.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1.8f;
            playerMovement.animator.SetBool("FlyClick", true);
        }

        if (playerMovement.animator.GetBool("FlyClick"))
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            playerMovement.animator.SetBool("FlyClick", false);
            timer = 0.35f;
        }

        if(playerMovement.died)
        {
            playerCame = false;
            rocket.SetActive(true);
            playerMovement.canJump = true;
            playerMovement.animator.SetBool("Fly", false);
            playerMovement.GetComponent<Rigidbody2D>().gravityScale = 3f;
        }

        if(playerMovement.wave3Complate)
        {
            playerCame = false;
        }

    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "Player" )
        {
            playerCame = true;
            playerMovement.GetComponent<Rigidbody2D>().gravityScale = -0.3f;
            playerMovement.canJump = false;
            playerMovement.animator.SetBool("Fly", true);
            rocket.SetActive(false);
        }
    }
}
