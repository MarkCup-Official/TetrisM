using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt_1 : MonoBehaviour
{
    float timmer = 0;
    int count=0;
    bool exp = false;
    public GameObject r;
    public GameObject w;
    public Rigidbody2D rig;

    private void Start()
    {
        rig.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 4f));
        MySystem.sound.PlayOneShot(MySystem.fuse);
        name = "Tnt_1";
    }

    private void Update()
    {
        if (!exp)
        {
            timmer += Time.deltaTime;
            if (timmer < 0.3)
            {
                r.SetActive(true);
                w.SetActive(false);
            }
            else if (timmer < 0.6)
            {
                r.SetActive(false);
                w.SetActive(true); 
                if (count >= 7)
                {
                    exp = true;
                }
            }
            else
            {
                timmer = 0;
                count++;
            }
        }
        else
        {
            timmer += Time.deltaTime;
            w.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            if (timmer > 0.5f)
            {
                MyCamera.shake = 0.1f;
                int sound = Random.Range(1, 5);
                switch (sound)
                {
                    case 1:
                        MySystem.sound.PlayOneShot(MySystem.exp1);
                        break;

                    case 2:
                        MySystem.sound.PlayOneShot(MySystem.exp2);
                        break;

                    case 3:
                        MySystem.sound.PlayOneShot(MySystem.exp3);
                        break;

                    case 4:
                        MySystem.sound.PlayOneShot(MySystem.exp4);
                        break;
                }
                Instantiate(MySystem.tnt_2, transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(gameObject);
            }
        }
    }
}
