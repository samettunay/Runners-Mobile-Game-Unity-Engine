using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    private Vector3 targetPosition, mousePos;
    public bool isSwimming = false, canSwim = false, colliderOpen = false;
    public PlayerMovement playerMovement;
    //bool closeThis = false;
    //float closeTimer = 0f;
    public vCam vcam;

    void Update()
    {
        colliderOpen = gameObject.GetComponent<BoxCollider2D>().enabled;

        if (canSwim)
            SetTargetPosition();

        if (isSwimming)
            Move();

        if (canSwim)
        {
            vcam.SwimCam();
        }
        else if (!canSwim)
        {
            vcam.SwimOrginalCamera();
        }

      /*  if (closeThis)
        {
            closeTimer += Time.deltaTime;
        }

        if (closeTimer > 2f)
        {
            this.enabled = false;
        }*/

    }

    void SetTargetPosition()
    {
        mousePos = new Vector3(1500, Input.mousePosition.y, 0); 
        targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
        isSwimming = true;
    }


    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 6f * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Swimming")
        {
            canSwim = true;
            //closeThis = false;
            playerMovement.canJump = false;
            playerMovement.rb.gravityScale = 0;
            playerMovement.animator.SetBool("Swim", true);
            playerMovement.animator.SetBool("IsJumping2", false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Swimming")
        {
            canSwim = false;
            //closeThis = true;
            isSwimming = false;
            playerMovement.canJump = true;
            playerMovement.rb.gravityScale = 3;
            playerMovement.animator.SetBool("Swim", false);
            playerMovement.animator.SetBool("IsJumping2", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            if (!colliderOpen)
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 11f;
        }
    }



}
