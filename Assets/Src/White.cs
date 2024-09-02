using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : MonoBehaviour
{
    
    public SpriteRenderer s;
    float a = 1;

    private void FixedUpdate()
    {
        a -= 0.1f;
        if (a >= 0)
        {
            s.color = new Color(1, 1, 1, a);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
