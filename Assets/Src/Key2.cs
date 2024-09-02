using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2 : MonoBehaviour
{
    public GameObject up;
    public GameObject down;
    void Update()
    {
        if (MySystem.is2)
        {
            up.SetActive(false);
            down.SetActive(true);
        }
        else
        {
            up.SetActive(true);
            down.SetActive(false);
        }
    }
}
