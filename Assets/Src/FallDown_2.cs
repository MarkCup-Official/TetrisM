using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown_2 : MonoBehaviour
{

    public FallDown mother;
    public Vector2 position;
    public int x;
    public int y;
    public int toward;
    public int type;
    public int id;
    public SpriteRenderer spriteR;
    public GameObject white;

    Vector2 v;

    public void _Reset()
    {
        x = (int)position.x;
        y = (int)position.y;
        spriteR.sprite = MySystem.BlocksSprite[id];
        if (id == 58)
        {
            Instantiate(Resources.Load("Water") as GameObject,transform);
        }
    }

    private void Update()
    {
        transform.localPosition = new Vector3(Mathf.SmoothDamp(transform.localPosition.x, x*0.4f, ref v.x, 0.1f), Mathf.SmoothDamp(transform.localPosition.y, y*0.4f, ref v.y, 0.1f), 0);
    }

    public bool CanTurn()
    {

        int ax = x;
        int ay = y;
        switch (type)
        {

            case 0:
                return true;

            case 1:
                switch (toward)
                {
                    case 0:
                        ax += 1;
                        ay -= 1;
                        break;

                    case 1:
                        ax -= 1;
                        ay -= 1;
                        break;

                    case 2:
                        ax -= 1;
                        ay += 1;
                        break;

                    case 3:
                        ax += 1;
                        ay += 1;
                        break;
                }
                if(mother.x + ax < 10 && mother.x + ax >= 0&& mother.y + ay < 20 && mother.y + ay >= 0)
                {
                    if (MySystem.blocks[mother.x + ax, mother.y + ay] != 0&&MySystem.blocks[mother.x + ax, mother.y + ay] != 56 && MySystem.blocks[mother.x + ax, mother.y + ay] != 58)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if(mother.x + ax < 10 && mother.x + ax >= 0  && mother.y + ay >= 19)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            case 2:
                switch (toward)
                {
                    case 0:
                        ax += 2;
                        ay -= 2;
                        break;

                    case 1:
                        ax -= 2;
                        ay -= 2;
                        break;

                    case 2:
                        ax -= 2;
                        ay += 2;
                        break;

                    case 3:
                        ax += 2;
                        ay += 2;
                        break;
                }
                if (mother.x + ax < 10 && mother.x + ax >= 0 && mother.y + ay < 20 && mother.y + ay >= 0)
                {
                    if (MySystem.blocks[mother.x + ax, mother.y + ay] != 0 && MySystem.blocks[mother.x + ax, mother.y + ay] != 56 &&MySystem.blocks[mother.x + ax, mother.y + ay] != 58)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (mother.x + ax < 10 && mother.x + ax >= 0 && mother.y + ay >= 19)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            case 3:
                switch (toward)
                {
                    case 0:
                        ay -= 2;
                        break;

                    case 1:
                        ax -= 2;
                        break;

                    case 2:
                        ay += 2;
                        break;

                    case 3:
                        ax += 2;
                        break;
                }
                if (mother.x + ax < 10 && mother.x + ax >= 0 && mother.y + ay < 20 && mother.y + ay >= 0)
                {
                    if (MySystem.blocks[mother.x + ax, mother.y + ay] != 0&& MySystem.blocks[mother.x + ax, mother.y + ay] != 56&&MySystem.blocks[mother.x + ax, mother.y + ay] != 58)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (mother.x + ax < 10 && mother.x + ax >= 0 && mother.y + ay >= 19)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
        }
        return false;
    }

    public void Turn()
    {
        switch (type)
        {

            case 0:
                break;

            case 1:
                switch (toward)
                {
                    case 0:
                        x += 1;
                        y -= 1;
                        break;

                    case 1:
                        x -= 1;
                        y -= 1;
                        break;

                    case 2:
                        x -= 1;
                        y += 1;
                        break;

                    case 3:
                        x += 1;
                        y += 1;
                        break;
                }
                break;
                

            case 2:
                switch (toward)
                {
                    case 0:
                        x += 2;
                        y -= 2;
                        break;

                    case 1:
                        x -= 2;
                        y -= 2;
                        break;

                    case 2:
                        x -= 2;
                        y += 2;
                        break;

                    case 3:
                        x += 2;
                        y += 2;
                        break;
                }
                break;

            case 3:
                switch (toward)
                {
                    case 0:
                        y -= 2;
                        break;

                    case 1:
                        x -= 2;
                        break;

                    case 2:
                        y += 2;
                        break;

                    case 3:
                        x += 2;
                        break;
                }
                break;
        }

        toward += 1;
        if (toward > 3)
        {
            toward = 0;
        }
    }

    public bool CanLeft()
    {
        if (mother.x + x-1 < 10 && mother.x + x-1 >= 0 && mother.y + y < 25 && mother.y + y >= 0)
        {
            if (MySystem.blocks[mother.x + x-1, mother.y + y] != 0&& MySystem.blocks[mother.x + x - 1, mother.y + y] != 58&& MySystem.blocks[mother.x + x - 1, mother.y + y] != 56)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public bool CanRight()
    {
        if (mother.x + x + 1 < 10 && mother.x + x + 1 >= 0 && mother.y + y < 25 && mother.y + y >= 0)
        {
            if (MySystem.blocks[mother.x + x + 1, mother.y + y] != 0&& MySystem.blocks[mother.x + x + 1, mother.y + y] != 56&& MySystem.blocks[mother.x + x + 1, mother.y + y] != 58)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public bool CanFall()
    {
        if (mother.x + x  < 10 && mother.x + x  >= 0 && mother.y + y-1 < 20 && mother.y + y-1 >= 0)
        {
            if (MySystem.blocks[mother.x + x , mother.y + y-1] != 0)
            {
                if (MySystem.blocks[mother.x + x, mother.y + y - 1] == 5)
                {
                    int sound = Random.Range(1, 4);
                    switch (sound)
                    {
                        case 1:
                            MySystem.sound.PlayOneShot(MySystem.glass1);
                            break;
                        case 2:
                            MySystem.sound.PlayOneShot(MySystem.glass2);
                            break;
                        case 3:
                            MySystem.sound.PlayOneShot(MySystem.glass3);
                            break;
                    }
                    GameObject.Find((mother.x + x).ToString() + ',' + (mother.y + y - 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((mother.x + x).ToString() + ',' + (mother.y + y - 1).ToString()).GetComponent<Blocks>()._Reset();
                    return true;
                }
                if (MySystem.blocks[mother.x + x, mother.y + y - 1] == 56|| MySystem.blocks[mother.x + x, mother.y + y - 1] == 58)
                {
                    return true;
                }
                return false;
            }
        }
        else if(mother.x + x < 10 && mother.x + x >= 0 && mother.y + y - 1>=20)
        {
            return true;
        }
        else
        {
            return false;
        }
        return true;
    }

    public void Down()
    {
        if (y + mother.y > 19)
        {
            MySystem.gameOver = true;
            MySystem.isStart = false;
            mother.Destroy();
            Destroy(gameObject);
            return;
        }
        else if (id != 56&& id != 58)
        {
            white.SetActive(true);
            //Debug.Log(name + ":Success White");

            transform.SetParent(GameObject.Find("BlockMother").transform);
            //Debug.Log(name + ":Success Change parent");

            transform.name = (x + mother.x).ToString() + ',' + (y + mother.y).ToString();
            //Debug.Log(name + ":Success Change name");

            MySystem.blocks[x + mother.x, y + mother.y] = id;
            //Debug.Log(name + ":Success Add block list");

            transform.position = new Vector3(-2.8f + (x + mother.x) * 0.4f, -3.8f + (y + mother.y) * 0.4f);
            MoveTo moveTo = gameObject.AddComponent<MoveTo>();
            moveTo.position = new Vector2(-2.8f + (x + mother.x) * 0.4f, -3.8f + (y + mother.y) * 0.4f);
            moveTo.v = v;
            //Debug.Log(name + ":Success Move position");

            Blocks blocks = gameObject.AddComponent<Blocks>();
            blocks.x = x + mother.x;
            blocks.y = y + mother.y;
            blocks.id = id;
            blocks._Reset();
            //Debug.Log(name + ":Success Add block component");

            MySystem.ticks[x + mother.x, y + mother.y] = true;
            //Debug.Log(name + ":Success Add block tick");

            //Debug.Log(name + ":Success Down");
            Destroy(this);
        }
        else if (id==56)
        {
            Lava.AddWater(mother.x+x, mother.y+y);
            Destroy(gameObject);
        }
        else if (id == 58)
        {
            Water.AddWater(mother.x+x, mother.y+y);
            Destroy(gameObject);
        }
    }

}
