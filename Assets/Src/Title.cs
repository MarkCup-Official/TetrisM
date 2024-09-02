using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{

    public Vector2 position;
    public float waitTime;
    public float speed;
    public Rigidbody2D r;
    public bool move;
    public bool isDown;
    public bool isMother;
    Vector2 v;
    bool isOnPosition=false;
    float timemer=0;
    float movePosition;

    private void Start()
    {
        if (!isMother)
        {
            r.simulated = false;
        }
        else
        {
            MySystem.StartLoadScene("Game1");
        }
    }

    private void Update()
    {
        if (!isMother)
        {
            timemer += Time.deltaTime;
            if (timemer >= waitTime && !isOnPosition)
            {
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, position.x, ref v.x, speed), Mathf.SmoothDamp(transform.position.y, position.y, ref v.y, speed), 0);
                if (Mathf.Abs(transform.position.x - position.x) + Mathf.Abs(transform.position.y - position.y) <= 0.01f)
                {
                    transform.position = position;
                    isOnPosition = true;
                }
            }
            else if (isOnPosition)
            {
                if (!GameObject.Find("Title").GetComponent<Title>().isDown)
                {
                    if (move)
                    {
                        if (transform.position.y - position.y > 0.059)
                        {
                            movePosition = position.y - 0.06f;
                            v.y = 0;
                        }
                        else if (transform.position.y - position.y < -0.059)
                        {
                            movePosition = position.y + 0.06f;
                            v.y = 0;
                        }
                        else if (transform.position.y - position.y == 0)
                        {
                            movePosition = position.y - 0.06f;
                            v.y = 0;
                        }
                        transform.position = new Vector3(position.x, Mathf.SmoothDamp(transform.position.y, movePosition, ref v.y, 1f), 0);
                    }
                }
                else
                {
                    r.simulated = true;
                    r.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(5, 10f));
                    r.angularVelocity = Random.Range(-50f, 50f);
                    Destroy(this);
                }
            }
        }
        else
        {
            if (isDown)
            {
                timemer += Time.deltaTime;
                if (timemer > 1.3)
                {
                    MySystem.LoadScene();
                    Destroy(this);
                }
            }
        }
    }

}
