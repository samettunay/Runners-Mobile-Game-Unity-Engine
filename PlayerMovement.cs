using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    private Vector3 tempPosition;
    public GameObject panel, heart1, heart2, heart3, rock, rock2;
    public Rigidbody2D rb;
    public Animator animator;
    public vCam vcam;
    public Swim swim;
    public HorseCalling horseCalling;
    //float startTimer = 0f;
    float  dieTimer = 0f, slowTimer = 2f;
    public float runSpeed = 0f, jumpTimer = 0f, airTimer = 0f;
    public bool start = false, checkDead = false, stop = false, died = false, slowMarkerCol = false, speedMarkerCol = false, canJump = true, wave3Complate = false;
    public bool onTile = false;
    public byte dieCounter = 0;


    void Start()
    {
        tempPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        FreezePlayer();
        //stop = true;
        //canJump = false;
    }

    void Update()
    {
        //if (!start)
            //startTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            UnFreezePlayer();
            start = true;
        }
            

        if (start && !died && !checkDead)
        {
            if (animator.GetBool("onHorse"))
            {
                runSpeed = 10f;
                gameObject.GetComponent<CircleCollider2D>().radius = 1.17f;
            }
            else if (stop)
            {
                runSpeed = 0f;
            }
            else if(swim.canSwim)
            {
                runSpeed = 1f;
            }
            else
            {
                runSpeed = 6f;
                gameObject.GetComponent<CircleCollider2D>().radius = 0.6113054f;
            }

            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }

        animator.SetFloat("Speed", Mathf.Abs(runSpeed));
        if(animator.GetBool("IsJumping2"))
        jumpTimer += Time.deltaTime;

        // JUMP

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("up")) && !animator.GetBool("IsJumping2") && runSpeed > 0f && !animator.GetBool("onHorse") && canJump)
        {
            jumpTimer = 0f;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
            animator.SetBool("IsJumping2", true);
            onTile = false;
        }

       
        if (died)
            dieTimer += Time.deltaTime;


        if (dieTimer > 1f)
        {
            transform.position = tempPosition;
            if (!checkDead)
                gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
            animator.SetBool("asitDeath", false);
            died = false;
            dieTimer = 0f;
        }

        #region Markers

        if (slowMarkerCol)
        {
            Time.timeScale = 0.5f;
            slowTimer -= Time.deltaTime;
            if(slowTimer < 0)
            {
                Time.timeScale = 1f;
                slowMarkerCol = false;
                slowTimer = 2f;
            }
        }

        if (speedMarkerCol)
        {
            Time.timeScale = 1.5f;
            slowTimer -= Time.deltaTime;
            if(slowTimer < 0)
            {
                Time.timeScale = 1f;
                speedMarkerCol = false;
                slowTimer = 2f;
            }
        }

        #endregion

        if(!onTile)      
            airTimer += Time.deltaTime;
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        #region TriggerTags
        if (other.tag == "Tile")
        {
            animator.SetBool("IsJumping2", false);
            onTile = true;
            airTimer = 0;
        }

        // Die

        if (other.tag == "asitDie")
        {
            Die();
            AfterToDie();
        }

        // Check Point

        if (other.tag == "CheckPoint")
        {
            tempPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        // JUMP MARKER

        if (other.tag == "JumpMarker" && jumpTimer > 0.2f)
        {
            jumpTimer = 0f;
            animator.SetBool("IsJumping2", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 15f;
        }

        if (other.tag == "DownJumpMarker")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 25f;
        }

        //Shake

        if (other.tag == "Cave")
        {
            vcam.ShakeStart();
        }

        if (other.tag == "CatchCircle")
        {
            horseCalling.controle = true;
            stop = true;
        }

        if (other.tag == "RockComing")
        {
            rock.SetActive(true);
        }

        if (other.tag == "downOnHorse")
        {
            vcam.ShakeStop();
            animator.SetBool("onHorse", false); stop = false;
        }

        if (other.tag == "SlowMarker")
        {
           slowMarkerCol = true;
        }
        else if(other.tag == "SpeedMarker")
        {
            speedMarkerCol = true;
        }

        // Wave 3

        if (other.tag == "Wave3Complate")
        {
            wave3Complate = true;
            canJump = true;
            animator.SetBool("Fly", false);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3f;
        }
    #endregion
        if(other.tag == "Swimming")
        {
            gameObject.GetComponent<Swim>().enabled = true;
        }
    }

    public void Die()
    {
        animator.SetBool("asitDeath", true);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        died = true; dieCounter++;
        rock.SetActive(false);
        rock2.SetActive(false);
        AfterToDie();
    }

    public void FreezePlayer()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
    public void UnFreezePlayer()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;

    }

    void AfterToDie()
    {
        if (dieCounter == 1 && heart3.activeSelf)
        {
            heart3.SetActive(false);
        }
        else if (dieCounter == 2 && heart2.activeSelf)
        {
            heart2.SetActive(false);
        }
        else if (dieCounter == 3 && heart1.activeSelf && !panel.activeSelf)
        {
            runSpeed = 0f;
            checkDead = true;
            heart1.SetActive(false);
            panel.SetActive(true);
        }
    }
}

internal struct NewStruct
{
    public object Item1;
    public object Item2;

    public NewStruct(object ıtem1, object ıtem2)
    {
        Item1 = ıtem1;
        Item2 = ıtem2;
    }

    public override bool Equals(object obj)
    {
        return obj is NewStruct other &&
               System.Collections.Generic.EqualityComparer<object>.Default.Equals(Item1, other.Item1) &&
               System.Collections.Generic.EqualityComparer<object>.Default.Equals(Item2, other.Item2);
    }

    public override int GetHashCode()
    {
        int hashCode = -1030903623;
        hashCode = hashCode * -1521134295 + System.Collections.Generic.EqualityComparer<object>.Default.GetHashCode(Item1);
        hashCode = hashCode * -1521134295 + System.Collections.Generic.EqualityComparer<object>.Default.GetHashCode(Item2);
        return hashCode;
    }

    public void Deconstruct(out object ıtem1, out object ıtem2)
    {
        ıtem1 = Item1;
        ıtem2 = Item2;
    }

    public static implicit operator (object, object)(NewStruct value)
    {
        return (value.Item1, value.Item2);
    }

    public static implicit operator NewStruct((object, object) value)
    {
        return new NewStruct(value.Item1, value.Item2);
    }
}