using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    Vector2 v;
    bool isOnPosition=false;
    Vector3 position=new Vector3(0,0,-10);
    public static float shake=0;

    private void Update()
    {
        position.z = -10;
        if (!isOnPosition)
        {
            transform.position = new Vector3(0, Mathf.SmoothDamp(transform.position.y, 0, ref v.y, 0.5f), -10);
            if (Mathf.Abs(transform.position.y) < 0.001)
            {
                isOnPosition = true;
                transform.position = new Vector3(0, 0, -10);
            }
        }
        else
        {
            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, position.x, ref v.x, 0.5f), Mathf.SmoothDamp(transform.position.y, position.y, ref v.y, 0.5f), -10);
            if (Mathf.Abs(transform.position.x - position.x) + Mathf.Abs(transform.position.y - position.y) <= 0.001f)
            {
                transform.position = position;
            }
        }
    }
    private void FixedUpdate()
    {
        if (shake > 0)
        {
            transform.position = new Vector3(position.x + Random.Range(-0.2f, 0.2f), position.y + Random.Range(-0.2f, 0.2f), -10);
            shake -= Time.deltaTime;
        }
    }

    public void Shake(float x, float y)
    {
        transform.position = new Vector3(x, y,-10);
    }

}
