using UnityEngine;
using System.Collections;

public class ParticleEffects : MonoBehaviour
{
    public ParticleSystem explationEffect;
    public GameObject secondRock, thisRock;
    public AudioSource rockBrokerSfx;
    public AudioClip rockSfx;
    public Wave2 wave2;

    void OnMouseDown()
    {
        Explation();
    }

    public void Explation()
    {
        explationEffect.Play();
        rockBrokerSfx.clip = rockSfx;
        rockBrokerSfx.Play();
        thisRock.SetActive(false);
        secondRock.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RockBroker")
        {
            Explation();
            wave2.levelNotComplated = true;
        }
    }

}
