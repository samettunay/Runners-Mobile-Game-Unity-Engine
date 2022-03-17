using UnityEngine;
using Cinemachine;
using System.Collections;

public class vCam : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float screenX, screenY, screenXOrginal, screenYOrginal;
    public bool setLookUpAndMid = false, setOrginalCam = false;

    void Start()
    {
        screenX = 0.26f;
        screenY = 0.7f;
    }

    void Update()
    {
        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = screenX;
        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = screenY;

        if (setLookUpAndMid)
        {
            StartCoroutine(LookUpAndMid());
        }
        else if (setOrginalCam)
        {
            StartCoroutine(OrginalCamera());
        }
    }

    public IEnumerator LookUpAndMid()
    {
        if (screenX < 0.5f)
        {
            screenX += Time.deltaTime * 0.5f;
        }

        yield return new WaitForSeconds(1);

        if (screenY < 1.4f)
        {
            screenY += Time.deltaTime * 1.5f;
        }
    }

    public IEnumerator OrginalCamera()
    {
        if (screenY >= 0.7f)
        {
            screenY -= Time.deltaTime * 1.5f;
        }

        yield return new WaitForSeconds(2);

        if (screenX >= 0.26f)
        {
            screenX -= Time.deltaTime * 0.5f;
        }

    }

    public void ShakeStart()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
    }

    public void ShakeStop()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }

    public void SwimCam()
    {
        if (screenY > 0.5f)
        {
            screenY -= Time.deltaTime * 0.1f;
        }
    }

    public void SwimOrginalCamera()
    {
        if (screenY <= 0.7f)
        {
            screenY += Time.deltaTime * 0.1f;
        }
    }
}