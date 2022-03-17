using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graple : MonoBehaviour
{

    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public PlayerMovement playerMovement;
    public bool canHook = false;
    private bool checkTimer = false;
    public float hookTimer = 0f;
    Vector3 hookPos;

    void Start()
    {
        _distanceJoint.enabled = false;
    }

    void Update()
    {
        hookPos = new Vector3(transform.position.x + 0.217f, transform.position.y + 0.19f, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Mouse0) && canHook && playerMovement.airTimer > 0.3f)
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            playerMovement.animator.SetBool("Hook", true);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, hookPos);
            _distanceJoint.connectedAnchor = mousePos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
            checkTimer = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            mouseUp();
            hookTimer = 0;
            checkTimer = false;
        }
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, hookPos);
        }

        if (checkTimer)
            hookTimer += Time.deltaTime;

        if (hookTimer > 1.7f)
        {
            hookTimer = 0;
            checkTimer = false;
            mouseUp();
        }

    }

    public void mouseUp()
    {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
        playerMovement.animator.SetBool("Hook", false);
    }

}
