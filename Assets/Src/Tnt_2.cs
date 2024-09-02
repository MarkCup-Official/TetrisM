using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt_2 : MonoBehaviour
{
    void Start()
    {
        transform.localPosition = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f));
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        float scale = Random.Range(1f, 3f);
        transform.localScale = new Vector3(scale, scale);
    }
}
