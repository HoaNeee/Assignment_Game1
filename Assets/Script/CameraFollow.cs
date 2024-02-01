using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothing;

    Vector3 offset;
    float lowY;
    public float minX, maxX, minY, maxY;
    void Start()
    {
        if (player == GameObject.Find("Player").transform)
        {
            offset = transform.position - player.position;
            //player = GameObject.Find("Player").transform;

            lowY = transform.position.y;
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 vector3 = transform.position;
            vector3.x = player.position.x;


            if (vector3.x < minX)
            {
                vector3.x = minX;

            }
            if (vector3.x > maxX)
            {
                vector3.x = maxX;

            }
            transform.position = vector3;
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetCam = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCam, smoothing * Time.deltaTime);

        if(transform.position.y < lowY)
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
        if (transform.position.y > 1.20)
        {
            transform.position = new Vector3(transform.position.x, (float)1.20, transform.position.z);
        }
    }
}
