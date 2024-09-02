using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key3 : MonoBehaviour
{
    public GameObject up;
    public GameObject down;
    void Update()
    {
        if (MySystem.is1)
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
