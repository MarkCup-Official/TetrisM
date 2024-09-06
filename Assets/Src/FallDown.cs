using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{

    public bool isShow=true;
    public int toward;//0:ио 1:ср 2:об 3:вС
    public int type;//0:i 1:o 2:t 3:l 4:j 5:s 6:z
    public int x;
    public int y;
    public FallDown_2 a;
    public FallDown_2 b;
    public FallDown_2 c;
    public FallDown_2 d;
    public int id=0;
    public bool canNext;
    public GameObject next;
    int timmer;
    int timmer2;
    Vector2 v;

    private void Start()
    {
        MySystem.isHold = true;
        int types = Random.Range(1, 3);
        if (types == 1)
        {
            while (id == 0 || id == 4 || id == 7 || id == 12 || id == 13 || id == 14 || id == 57)
            {
                id = Random.Range(1, 23);
                if (id >= 18)
                {
                    id += 36;
                }
            }
        }
        else
        {
            while (id == 0 || id == 4 || id == 7 || id == 13 || id == 14 || id == 49)
            {
                id = Random.Range(18, 53);
            }
        }
        if(GameObject.Find("FallDown"))
        if (GameObject.Find("FallDown").GetComponent<FallDown>().id == 1)
        {
            int r= Random.Range(1, 100);
            if (r < 30)
            {
                id = 56;
            }
            else if(r < 60)
            {
                id = 2;
            }
            else if (r < 90)
            {
                id = 15;
            }
        }
        a.id = id;
        b.id = id;
        c.id = id;
        d.id = id;
        /*if (Random.Range(1, 100) == 3)
        {
            a.id = 57;
        }
        else if (Random.Range(1, 100) == 3)
        {
            a.id = 12;
        }
        if (Random.Range(1, 100) == 3)
        {
            b.id = 57;
        }
        else if (Random.Range(1, 100) == 3)
        {
            b.id = 12;
        }
        if (Random.Range(1, 100) == 3)
        {
            c.id = 57;
        }
        else if (Random.Range(1, 100) == 3)
        {
            c.id = 12;
        }
        if (Random.Range(1, 100) == 3)
        {
            d.id = 57;
        }
        else if (Random.Range(1, 100) == 3)
        {
            d.id = 12;
        }*/
        if (!MySystem.hadGod)
        {
            if (Random.Range(1, 100) == 3)
            {
                a.id = 12;
                MySystem.hadGod = true;
            }
            else if (Random.Range(1, 100) == 3)
            {
                b.id = 12;
                MySystem.hadGod = true;
            }
            else if (Random.Range(1, 100) == 3)
            {
                c.id = 12;
                MySystem.hadGod = true;
            }
            else if (Random.Range(1, 100) == 3)
            {
                d.id = 12;
                MySystem.hadGod = true;
            }
        }

        x = 5;
        y = 21;
        a.position = new Vector2(0,0);
        a.toward = 0;
        a.type = 0;
        switch (type)
        {
            case 0:
                b.position = new Vector2(0, 2);
                b.toward = 0;
                b.type = 2;
                c.position = new Vector2(0, 1);
                c.toward = 0;
                c.type = 1;
                d.position = new Vector2(0, -1);
                d.toward = 2;
                d.type = 1;
                break;

            case 1:
                b.position = new Vector2(0, 1);
                b.toward = 0;
                b.type = 1;
                c.position = new Vector2(1, 1);
                c.toward = 0;
                c.type = 3;
                d.position = new Vector2(1, 0);
                d.toward = 1;
                d.type = 1;
                break;

            case 2:
                b.position = new Vector2(-1, 0);
                b.toward = 3;
                b.type = 1;
                c.position = new Vector2(1, 0);
                c.toward = 1;
                c.type = 1;
                d.position = new Vector2(0, -1);
                d.toward = 2;
                d.type = 1;
                break;

            case 3:
                b.position = new Vector2(0, 1);
                b.toward = 0;
                b.type = 1;
                c.position = new Vector2(0, -1);
                c.toward = 2;
                c.type = 1;
                d.position = new Vector2(1, -1);
                d.toward = 1;
                d.type = 3;
                break;

            case 4:
                b.position = new Vector2(0, 1);
                b.toward = 0;
                b.type = 1;
                c.position = new Vector2(-1, -1);
                c.toward = 2;
                c.type = 3;
                d.position = new Vector2(0, -1);
                d.toward = 2;
                d.type = 1;
                break;

            case 5:
                b.position = new Vector2(1, 0);
                b.toward = 1;
                b.type = 1;
                c.position = new Vector2(-1, -1);
                c.toward = 2;
                c.type = 3;
                d.position = new Vector2(0, -1);
                d.toward = 2;
                d.type = 1;
                break;

            case 6:
                b.position = new Vector2(-1, 0);
                b.toward = 3;
                b.type = 1;
                c.position = new Vector2(0, -1);
                c.toward = 2;
                c.type = 1;
                d.position = new Vector2(1, -1);
                d.toward = 1;
                d.type = 3;
                break;

        }
        a._Reset();
        b._Reset();
        c._Reset();
        d._Reset();
    }

    private void Update()
    {
        if (isShow)
        {
            name = "Showing";
            switch (type)
            {
                case 0:
                    transform.position = new Vector3(2, 0.8f, 0);
                    break;
                case 1:
                    transform.position = new Vector3(1.8f, 0.8f, 0);
                    break;
                case 2:
                    transform.position = new Vector3(2, 1.2f, 0);
                    break;
                case 3:
                    transform.position = new Vector3(1.8f, 1, 0);
                    break;
                case 4:
                    transform.position = new Vector3(2.2f, 1, 0);
                    break;
                case 5:
                    transform.position = new Vector3(2, 1.2f, 0);
                    break;
                case 6:
                    transform.position = new Vector3(2, 1.2f, 0);
                    break;
            }
        }
        else
        {
            if (!MySystem.isBack)
            {
                name = "FallDown";
                if ((Input.GetKeyDown(MySystem.keys.w) || Input.GetKeyDown(MySystem.keys.up) || MySystem.buttonDown[1] == 1) && !MySystem.hadTurn)
                {
                    Turn();
                    MySystem.hadTurn = true;
                }
                if (!(Input.GetKeyDown(MySystem.keys.w) || Input.GetKeyDown(MySystem.keys.up) || MySystem.buttonDown[1] == 1) && MySystem.hadTurn)
                {
                    MySystem.hadTurn = false;
                }

                if ((Input.GetKeyDown(MySystem.keys.space) || MySystem.buttonDown[2] == 1) && !MySystem.hadDown)
                {
                    FallToEnd();
                    MySystem.hadDown = true;
                }
                if (!(Input.GetKeyDown(MySystem.keys.space) || MySystem.buttonDown[2] == 1) && MySystem.hadDown)
                {
                    MySystem.hadDown = false;
                }

                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, -2.8f + x * 0.4f, ref v.x, 0.1f), Mathf.SmoothDamp(transform.position.y, -3.8f + y * 0.4f, ref v.y, 0.1f), 0);
            }
            
        }
        
    }

    private void FixedUpdate()
    {
        if ((!isShow)&&(!MySystem.isBack))
        {
            timmer++;
            if (timmer >= MySystem.speed)
            {
                timmer = 0;
                Fall();
            }

            if (Input.GetKey(MySystem.keys.s) || Input.GetKey(MySystem.keys.down) || MySystem.buttonDown[0] == 4)
            {
                if (timmer >= 5)
                {
                    timmer = 0;
                    Fall();
                }
            }
            int move = 0;
            if (Input.GetKey(MySystem.keys.a) || Input.GetKey(MySystem.keys.left) || MySystem.buttonDown[0] == 2)
            {
                move = 1;
            }
            if (Input.GetKey(MySystem.keys.d) || Input.GetKey(MySystem.keys.right) || MySystem.buttonDown[0] == 3)
            {

                if (move == 1)
                {
                    move = 0;
                }
                else
                {
                    move = 2;
                }
            }
            if (move == 0)
            {
                timmer2 = 100;
            }
            else
            {
                timmer2++;
            }
            if (timmer2 >= 10 && move != 0)
            {
                timmer2 = 0;
                if (move == 1)
                {
                    Left();
                }
                else if (move == 2)
                {
                    Right();
                }
            }
        }
        
    }

    public void EndShow()
    {
        transform.position = new Vector3(0, 100, 0);
    }

    public void Turn()
    {
        int sound = Random.Range(1, 5);
        switch (sound)
        {
            case 1:
                MySystem.sound2.PlayOneShot(MySystem.turn1);
                break;
            case 2:
                MySystem.sound2.PlayOneShot(MySystem.turn2);
                break;
            case 3:
                MySystem.sound2.PlayOneShot(MySystem.turn3);
                break;
            case 4:
                MySystem.sound2.PlayOneShot(MySystem.turn4);
                break;
        }
        if (a.CanTurn() && b.CanTurn() && c.CanTurn() && d.CanTurn())
        {
            a.Turn();
            b.Turn();
            c.Turn();
            d.Turn(); 
        }
        toward += 1;
        if (toward > 3)
        {
            toward = 0;
        }
    }

    public void Fall()
    {
        if(a.CanFall() && b.CanFall() && c.CanFall() && d.CanFall())
        {
            y -= 1;
            //Debug.Log( name+":Success Fall");
        }
        else
        {
            //Debug.Log(name + ":Call Down");
            Down();
        }
    }

    public void Left()
    {
        if (a.CanLeft() && b.CanLeft() && c.CanLeft() && d.CanLeft())
        {
            x -= 1;
        }
    }

    public void Right()
    {
        if (a.CanRight() && b.CanRight() && c.CanRight() && d.CanRight())
        {
            x += 1;
        }
    }


    public void Down()
    {
        //Debug.Log(name + ":Call 1 Down");
        a.Down();
        //Debug.Log(name + ":Call 2 Down");
        b.Down();
        //Debug.Log(name + ":Call 3 Down");
        c.Down();
        //Debug.Log(name + ":Call 4 Down");
        d.Down();
        if(canNext)
            Instantiate(next);
        //Debug.Log(name + ":Call Camera Shake");
        GameObject.Find("Main Camera").GetComponent<MyCamera>().Shake(0,0.1f);
        MySystem.isHold = false;
        //Debug.Log(name + ":Success Down");
        if (id==1 || id == 3 || id == 4 || id == 10 || id == 15)
        {
            int sound = Random.Range(1, 5);
            switch (sound)
            {
                case 1:
                    MySystem.sound.PlayOneShot(MySystem.grass1);
                    break;
                case 2:
                    MySystem.sound.PlayOneShot(MySystem.grass2);
                    break;
                case 3:
                    MySystem.sound.PlayOneShot(MySystem.grass3);
                    break;
                case 4:
                    MySystem.sound.PlayOneShot(MySystem.grass4);
                    break;
            }
        }
        else if (id==8)
        {
            int sound = Random.Range(1, 5);
            switch (sound)
            {
                case 1:
                    MySystem.sound.PlayOneShot(MySystem.sand1);
                    break;
                case 2:
                    MySystem.sound.PlayOneShot(MySystem.sand2);
                    break;
                case 3:
                    MySystem.sound.PlayOneShot(MySystem.sand3);
                    break;
                case 4:
                    MySystem.sound.PlayOneShot(MySystem.sand4);
                    break;
            }
        }
        else if (id == 11 || id == 19 || id == 20 )
        {
            int sound = Random.Range(1, 5);
            switch (sound)
            {
                case 1:
                    MySystem.sound.PlayOneShot(MySystem.wood1);
                    break;
                case 2:
                    MySystem.sound.PlayOneShot(MySystem.wood2);
                    break;
                case 3:
                    MySystem.sound.PlayOneShot(MySystem.wood3);
                    break;
                case 4:
                    MySystem.sound.PlayOneShot(MySystem.wood4);
                    break;
            }
        }
        else if ((id % 2 == 0 && id >= 26 && id <= 34) || (id % 2 == 1 && id >= 37 && id <= 41) || (id >= 42&& id <= 48) ||id==54)
        {
            int sound = Random.Range(1, 5);
            switch (sound)
            {
                case 1:
                    MySystem.sound.PlayOneShot(MySystem.cloth1);
                    break;
                case 2:
                    MySystem.sound.PlayOneShot(MySystem.cloth2);
                    break;
                case 3:
                    MySystem.sound.PlayOneShot(MySystem.cloth3);
                    break;
                case 4:
                    MySystem.sound.PlayOneShot(MySystem.cloth4);
                    break;
            }
        }
        else if (id == 56)
        {
            int sound = Random.Range(1, 4);
            switch (sound)
            {
                case 1:
                    MySystem.sound.PlayOneShot(MySystem.lava1);
                    break;
                case 2:
                    MySystem.sound.PlayOneShot(MySystem.lava2);
                    break;
                case 3:
                    MySystem.sound.PlayOneShot(MySystem.lava3);
                    break;
            }
        }
        else if (id == 58)
        {
            int sound = Random.Range(1, 4);
            switch (sound)
            {
                case 1:
                    MySystem.sound.PlayOneShot(MySystem.water1);
                    break;
                case 2:
                    MySystem.sound.PlayOneShot(MySystem.water2);
                    break;
                case 3:
                    MySystem.sound.PlayOneShot(MySystem.water3);
                    break;
            }
        }
        else// if (id == 2 || id == 5 || id == 6 || id == 7 || id == 9 || id == 12 || id == 13 || id == 14 || id == 16 || id == 17 || id == 18 || (id <= 25 && id >= 21) || (id % 2 == 1 && id >= 27 && id <= 35) || (id % 2 == 0 && id >= 36 && id <= 40) || id == 49 || id == 50 || id == 51 || id == 52 || id == 53 || id == 55 || id == 57)
        {
            int sound = Random.Range(1, 5);
            switch (sound)
            {
                case 1:
                    MySystem.sound.PlayOneShot(MySystem.stone1);
                    break;
                case 2:
                    MySystem.sound.PlayOneShot(MySystem.stone2);
                    break;
                case 3:
                    MySystem.sound.PlayOneShot(MySystem.stone3);
                    break;
                case 4:
                    MySystem.sound.PlayOneShot(MySystem.stone4);
                    break;
            }
        }
        Destroy(gameObject);
    }

    public void FallToEnd()
    {
        while(a.CanFall() && b.CanFall() && c.CanFall() && d.CanFall())
        {
            y -= 1;
        }
        Down();
        GameObject.Find("Main Camera").GetComponent<MyCamera>().Shake(0, 0.2f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
