using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Graple graple;


    void OnMouseDown()
    {
        graple.canHook = true;
    }

    void OnMouseUp()
    {
        graple.canHook = false;
    }

}
