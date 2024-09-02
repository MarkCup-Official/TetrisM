using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{

    public Vector2 position;
    public Vector2 v;

    void Update()
    {
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, position.x, ref v.x, 0.1f), Mathf.SmoothDamp(transform.position.y, position.y, ref v.y, 0.1f),0 );
        if (Mathf.Abs(transform.position.x - position.x) + Mathf.Abs(transform.position.y - position.y) <= 0.001f)
        {
            transform.position = position;
            Destroy(this);
        }
    }

}
