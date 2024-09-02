using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Title_2 : MonoBehaviour,IPointerClickHandler
{

    public Text s;
    public float time;
    float timmer=0;
    float a=0;

    private void Start()
    {
        s.color = new Color(1, 1, 1, 0);
    }

    private void FixedUpdate()
    {
        timmer += Time.deltaTime;
        if (a < 1&&timmer>=time&&!GameObject.Find("Title").GetComponent<Title>().isDown)
        {
            a += 0.01f;
            s.color = new Color(1, 1, 1, a);
        }
        else if(a > 0 && GameObject.Find("Title").GetComponent<Title>().isDown)
        {
            a -= 0.05f;
            if (a < 0)
            {
                a = 0;
            }
            s.color = new Color(1, 1, 1, a);
            
        }
        if (timmer >= time&&Input.anyKey)
        {
            //GameObject.Find("Title").GetComponent<Title>().isDown = true;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("Title").GetComponent<Title>().isDown = true;
    }
}
