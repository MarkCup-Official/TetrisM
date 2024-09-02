using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1 : MonoBehaviour
{
    public GameObject zero;
    public GameObject up;
    public GameObject left;
    public GameObject down;
    public GameObject right;
    private void Update()
    {
        switch (MySystem.buttonDown[0])
        {
            case 0:
                zero.SetActive(true);
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                break;
            case 1:
                zero.SetActive(false);
                up.SetActive(true);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                break;
            case 2:
                zero.SetActive(false);
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(true);
                right.SetActive(false);
                break;
            case 3:
                zero.SetActive(false);
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(true);
                break;
            case 4:
                zero.SetActive(false);
                up.SetActive(false);
                down.SetActive(true);
                left.SetActive(false);
                right.SetActive(false);
                break;
        }
    }
}
