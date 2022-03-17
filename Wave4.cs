using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave4 : MonoBehaviour
{
    private Vector3 objectPos;
    float posX, posY;
    bool right = true, left = false;
    void Start()
    {
        objectPos = transform.position;
        posX = transform.position.x;
        posY = transform.position.y;
        if (name == "AhtapotReverse")
        {
            right = false;
            left = true;
        }
    }

    void Update()
    {
        if( name == "Balik" )
        {
            if(right)
            transform.position = new Vector3 (transform.position.x + (Time.deltaTime * 2f), transform.position.y, transform.position.z);
            if(left)
            transform.position = new Vector3 (transform.position.x - (Time.deltaTime * 2f), transform.position.y, transform.position.z);
            
            if( posX + 3f <  transform.position.x )
            {
                right = false;
                left = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if ( posX - 3f >  transform.position.x )
            {
                right = true;
                left = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if( name == "Ahtapot" || name == "AhtapotReverse" )
        {
            if(right)
                transform.position = new Vector3 (transform.position.x, transform.position.y + (Time.deltaTime * 2f), transform.position.z);

            if(left)
                transform.position = new Vector3 (transform.position.x, transform.position.y - (Time.deltaTime * 2f), transform.position.z);
            
            if( posY + 3f <  transform.position.y )
            {
                right = false;
                left = true;
            }
            else if ( posY - 3f >  transform.position.y )
            {
                right = true;
                left = false;
            }
        }
    }
}
