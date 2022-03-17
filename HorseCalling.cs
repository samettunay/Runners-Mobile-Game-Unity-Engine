using UnityEngine;

public class HorseCalling : MonoBehaviour
{
    public GameObject circle1, circle2, player;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip[] whistle;
    public AudioClip complateWhistle;
    public SpriteRenderer redOrGreen;
    public bool complate = false, controle = false, inCircle = false;

    void Update()
    {
        if( controle && Input.GetMouseButtonDown(0) && (circle2.transform.position.x - 0.3f < circle1.transform.position.x && circle2.transform.position.x + 0.3f > circle1.transform.position.x) )
        {
            if(!inCircle)
            audioSource.PlayOneShot(complateWhistle); inCircle = true;
            animator.enabled = false;
            player.GetComponent<Animator>().SetBool("onHorse", true);
            complate = true; controle = false;
        }
        else if( controle && !complate && Input.GetMouseButtonDown(0) && (circle2.transform.position.x - 0.3f > circle1.transform.position.x || circle2.transform.position.x + 0.3f < circle1.transform.position.x) )
        {
            audioSource.clip = whistle[Random.Range(0, whistle.Length)];
            audioSource.Play();
        }

        if(circle2.transform.position.x - 0.3f < circle1.transform.position.x && circle2.transform.position.x + 0.3f > circle1.transform.position.x)
        {
            redOrGreen.color = new Color(0, 255, 0, 255);
        }
        else if(circle2.transform.position.x - 0.3f > circle1.transform.position.x || circle2.transform.position.x + 0.3f < circle1.transform.position.x)
        {
            redOrGreen.color = new Color(255, 0, 0, 255);
        }
    }

}
