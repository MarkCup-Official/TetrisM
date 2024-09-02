using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTimmer : MonoBehaviour
{
    public float time;
    float timmer = 0;
    private void Update()
    {
        timmer += Time.deltaTime;
        if (timmer > time)
        {
            Destroy(gameObject);
        }
    }
}
