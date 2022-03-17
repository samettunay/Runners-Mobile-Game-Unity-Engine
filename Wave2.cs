using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave2 : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject allRocks, lastRock;
    public GameObject[] rocks;
    public vCam vcam;
    public bool levelNotComplated = false;
    bool waveStart = false;
    float posY = 0, orginalPosY = 0;

    void Start()
    {
        posY =  allRocks.transform.position.y;
        orginalPosY =  allRocks.transform.position.y;
    }
    void Update()
    {
        allRocks.transform.position = new Vector3(allRocks.transform.position.x, posY, allRocks.transform.position.z);
        
        if (waveStart)
            posY = allRocks.transform.position.y - (Time.deltaTime * 2);
        
        if (!lastRock.activeSelf)
        {
            StartCoroutine(CompleteLevel());
        }

        if (levelNotComplated)
        {
            StartCoroutine(NotCompleteLevel());
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerMovement.stop = true;
            playerMovement.animator.SetBool("Saklan", true);
            vcam.setLookUpAndMid = true;
            vcam.ShakeStop();
            StartCoroutine(RocksComing());
        }
    }

    IEnumerator RocksComing()
    {
        yield return new WaitForSeconds(2);
        waveStart = true;
    }

    IEnumerator CompleteLevel()
    {
        vcam.setLookUpAndMid = false;
        vcam.setOrginalCam = true;
        playerMovement.animator.SetBool("Saklanma", true);
        yield return new WaitForSeconds(3);
        playerMovement.animator.SetBool("Saklan", false);
        playerMovement.stop = false;
    }

    IEnumerator NotCompleteLevel()
    {
        waveStart = false;
        vcam.ShakeStart();

        yield return new WaitForSeconds(2);

        vcam.ShakeStop();
        posY = orginalPosY;
        foreach (var rock in rocks)
            rock.SetActive(true);
        levelNotComplated = false;
        waveStart = true;
    }


}
