using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    public int id;
    public int x;
    public int y;
    float timmer15=0;
    bool isOn15 = false;
    int[,] before12=new int[10,20];
    bool isBack12=false;
    int timmer12,bx12,by12;
    int xx12 = 0, yy12 = 0;

    private void Update()
    {
        if (timmer15 > 0&&isOn15)
        {
            timmer15 -= Time.deltaTime;
            if (timmer15 <= 0)
            {
                isOn15 = false;
                MySystem.blocks[x, y] = 15;
                Add15();
            }
        }
    }

    public void Fall(int yy,int total)
    {
        int _y=1;
        bool canFall = true;
        while (y - _y >= yy)
        {
            if(MySystem.blocks[x, y - _y] == 9||id==9)
            {
                canFall = false;
            }
            _y++;
        }
        if (canFall)
        {
            MySystem.blocks[x, y] = 0;
            y -= total;
            if (y <= 19)
            {
                MySystem.ticks[x, y] = true;
            }

            _Reset();
        }
    }

    public void Tick()
    {
        int sound;
        switch (id)
        {
            case 1:
                if (Check(x + 1, y, 2) || Check(x - 1, y, 2) || Check(x, y + 1, 2) || Check(x, y - 1, 2)|| Check(x + 1, y, 56) || Check(x - 1, y, 56) || Check(x, y + 1, 56) || Check(x, y - 1, 56))
                {
                    id = 0;
                    Instantiate(MySystem.tnt, transform.position, Quaternion.Euler(0, 0, 0));
                    _Reset15();
                }
                break;

            case 3:
                if (Check(x , y+1, 0))
                {
                    id = 4;
                    _Reset();
                }
                break;

            case 4:
                if (!Check(x , y+1, 0))
                {
                    id = 3;
                    _Reset();
                }
                break;

            /*
        case 5://玻璃
            if (!(Check(x , y+1, 0) || Check(x, y + 1, 5)))
            {
                sound = Random.Range(1, 4);
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
                id = 0;
                _Reset();
                int _y = 0;
                while(y+_y<20&&MySystem.blocks[x, y + _y] != 9)
                {
                    if (MySystem.blocks[x, y + _y] != 0)
                    {
                        GameObject.Find(x.ToString() + ',' + (y + _y).ToString()).GetComponent<Blocks>().Fall(y,1);
                    }
                    _y++;
                }
            }
            break;
            */

            case 6:
                if (Check(x + 1, y, 2) || Check(x - 1, y, 2) || Check(x, y + 1, 2) || Check(x, y - 1, 2))
                {
                    id = 7;
                    _Reset15();
                }
                break;

            case 7:
                if (!(Check(x + 1, y, 2) || Check(x - 1, y, 2) || Check(x, y + 1, 2) || Check(x, y - 1, 2)))
                {
                    id = 6;
                    _Reset15();
                }
                break;

            case 8://沙子
                sound = 0;
                while(Check(x, y - 1, 0))
                {
                    Fall(y - 1, 1);
                    sound = Random.Range(1, 5);
                }
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
                break;

            case 11://木头
                if (Check(x + 1, y, 0))
                {
                    GameObject leave = Instantiate(MySystem.block);
                    leave.GetComponent<Blocks>().id = 10;
                    leave.GetComponent<Blocks>().x = x + 1;
                    leave.GetComponent<Blocks>().y = y;
                    leave.GetComponent<Blocks>()._Reset10();
                }
                if (Check(x - 1, y, 0))
                {
                    GameObject leave = Instantiate(MySystem.block);
                    leave.GetComponent<Blocks>().id = 10;
                    leave.GetComponent<Blocks>().x = x - 1;
                    leave.GetComponent<Blocks>().y = y;
                    leave.GetComponent<Blocks>()._Reset10();
                }
                if (Check(x, y+1, 0))
                {
                    GameObject leave = Instantiate(MySystem.block);
                    leave.GetComponent<Blocks>().id = 10;
                    leave.GetComponent<Blocks>().x = x ;
                    leave.GetComponent<Blocks>().y = y + 1;
                    leave.GetComponent<Blocks>()._Reset10();
                }
                if (Check(x , y-1, 0))
                {
                    GameObject leave = Instantiate(MySystem.block);
                    leave.GetComponent<Blocks>().id = 10;
                    leave.GetComponent<Blocks>().x = x ;
                    leave.GetComponent<Blocks>().y = y - 1;
                    leave.GetComponent<Blocks>()._Reset10();
                }
                break;

            case 12://结构方块
                id = 13;
                int xx = 0, yy = 0;
                while (xx < 10)
                {
                    before12[xx, yy] = MySystem.blocks[xx, yy];
                    yy++;
                    if (yy > 19)
                    {
                        yy = 0;
                        xx++;
                    }
                }
                bx12 = x;
                by12 = y;
                _Reset();
                break;

            case 15://标靶
                isOn15 = true;
                timmer15 = 0.5f;
                MySystem.blocks[x, y] = 2;
                Add15();
                break;

            case 16://南瓜头
                if(Check(x, y-1, 54)&& Check(x, y-2, 54))
                {
                    GameObject.Find(x .ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x .ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find(x .ToString() + ',' + (y - 2).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x .ToString() + ',' + (y - 2).ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.snowMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                else if(Check(x, y + 1, 54) && Check(x, y + 2, 54))
                {
                    GameObject.Find(x .ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x .ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find(x .ToString() + ',' + (y + 2).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x .ToString() + ',' + (y + 2).ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.snowMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                else if (Check(x-1, y, 54) && Check(x-2, y, 54))
                {
                    GameObject.Find((x -1).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x -1).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x -2).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x -2).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.snowMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                else if (Check(x+1, y, 54) && Check(x+2, y, 54))
                {
                    GameObject.Find((x + 1).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 1).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x + 2).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 2).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.snowMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                else if (Check(x, y - 1, 55) && Check(x, y - 2, 55) && Check(x - 1, y - 1, 55) && Check(x + 1, y - 1, 55))
                {
                    GameObject.Find(x.ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x.ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x + 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x - 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x - 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find(x.ToString() + ',' + (y - 2).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x.ToString() + ',' + (y - 2).ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.ironMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                else if (Check(x, y + 1, 55) && Check(x, y + 2, 55) && Check(x - 1, y + 1, 55) && Check(x + 1, y + 1, 55))
                {
                    GameObject.Find(x.ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x.ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x + 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x - 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x - 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find(x.ToString() + ',' + (y + 2).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find(x.ToString() + ',' + (y + 2).ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.ironMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                else if (Check(x + 1, y, 55) && Check(x + 2, y, 55) && Check(x + 1, y + 1, 55) && Check(x + 1, y - 1, 55))
                {
                    GameObject.Find((x + 1).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 1).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x + 2).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 2).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x + 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x + 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x + 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.ironMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                else if (Check(x - 1, y, 55) && Check(x - 2, y, 55) && Check(x - 1, y + 1, 55) && Check(x - 1, y - 1, 55))
                {
                    GameObject.Find((x - 1).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x - 1).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x - 2).ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x - 2).ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x - 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x - 1).ToString() + ',' + (y + 1).ToString()).GetComponent<Blocks>()._Reset();
                    GameObject.Find((x - 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>().id = 0;
                    GameObject.Find((x - 1).ToString() + ',' + (y - 1).ToString()).GetComponent<Blocks>()._Reset();
                    id = 0;
                    _Reset();
                    Instantiate(MySystem.ironMan, transform.position, Quaternion.Euler(Vector3.zero));
                }
                break;

            case 17://生草石头
                if (Check(x + 1, y, 58) || Check(x - 1, y, 58) || Check(x, y + 1, 58) || Check(x, y - 1, 58))
                {
                    id = 49;
                    _Reset();
                }
                break;

            case 49://bu生草石头
                if (Check(x + 1, y, 56) || Check(x - 1, y, 56) || Check(x, y + 1, 56) || Check(x, y - 1, 56))
                {
                    id = 17;
                    _Reset();
                }
                break;
        }

    }

    public void _Reset10()
    {
        Add();
        transform.SetParent(MySystem.BlockMother.transform);
        MySystem.blocks[x, y] = id;
        name = x.ToString() + ',' + y.ToString();
        gameObject.GetComponent<SpriteRenderer>().sprite = MySystem.BlocksSprite[id]; 
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
        transform.position= new Vector2(-2.8f + x * 0.4f, -3.8f + y * 0.4f);
    }

    public void _Reset()
    {
        Add();
        MySystem.blocks[x, y] = id;
        name = x.ToString() + ',' + y.ToString();
        gameObject.GetComponent<SpriteRenderer>().sprite = MySystem.BlocksSprite[id];
        if (gameObject.GetComponent<MoveTo>() == null)
        {
            gameObject.AddComponent<MoveTo>().position = new Vector2(-2.8f + x * 0.4f, -3.8f + y * 0.4f);
        }
        else
        {
            GetComponent<MoveTo>().position = new Vector2(-2.8f + x * 0.4f, -3.8f + y * 0.4f);
        }
        if (id == 0)
        {
            Destroy(gameObject);
        }
    }

    public void _Reset15()
    {
        Add15();
        MySystem.blocks[x, y] = id;
        name = x.ToString() + ',' + y.ToString();
        gameObject.GetComponent<SpriteRenderer>().sprite = MySystem.BlocksSprite[id];
        if (gameObject.GetComponent<MoveTo>() == null)
        {
            gameObject.AddComponent<MoveTo>().position = new Vector2(-2.8f + x * 0.4f, -3.8f + y * 0.4f);
        }
        else
        {
            GetComponent<MoveTo>().position = new Vector2(-2.8f + x * 0.4f, -3.8f + y * 0.4f);
        }
        if (id == 0)
        {
            Destroy(gameObject);
        }
    }

    bool Check(int x ,int y ,int id)
    {
        if (x >= 0 && x < 10 && y >= 0 && y < 20)
        {
            if (MySystem.blocks[x, y] == id)
            {
                return true;
            }
        }
        return false;
    }

    void Add()
    {
        if(x+1 >= 0 && x+1 < 10 && y >= 0 && y < 20)
        {
            if (MySystem.blocks[x + 1, y] != 0)
            {
                MySystem.ticks[x + 1, y] = true;
            }
        }
        if (x -1>= 0 && x - 1 < 10 && y >= 0 && y < 20)
        {
            if (MySystem.blocks[x -1, y] != 0)
            {
                MySystem.ticks[x - 1, y] = true;
            }
        }
        if (x  >= 0 && x  < 10 && y+1 >= 0 && y+1 < 20)
        {
            if (MySystem.blocks[x , y+1] != 0)
            {
                MySystem.ticks[x, y + 1] = true;
            }
        }
        if (x >= 0 && x< 10 && y-1 >= 0 && y-1 < 20)
        {
            if (MySystem.blocks[x , y-1] != 0)
            {
                MySystem.ticks[x, y - 1] = true;
            }
        }
    }

    void Add15()
    {
        if (x + 1 >= 0 && x + 1 < 10 && y >= 0 && y < 20)
        {
            if (MySystem.blocks[x + 1, y] != 0 && MySystem.blocks[x + 1, y] != 15 && MySystem.blocks[x + 1, y ] != 2)
            {
                MySystem.ticks[x + 1, y] = true;
            }
        }
        if (x - 1 >= 0 && x - 1 < 10 && y >= 0 && y < 20)
        {
            if (MySystem.blocks[x - 1, y] != 0 && MySystem.blocks[x - 1, y] != 15 && MySystem.blocks[x-1, y] != 2)
            {
                MySystem.ticks[x - 1, y] = true;
            }
        }
        if (x >= 0 && x < 10 && y + 1 >= 0 && y + 1 < 20)
        {
            if (MySystem.blocks[x, y + 1] != 0 && MySystem.blocks[x, y + 1] != 15 && MySystem.blocks[x, y + 1] != 2)
            {
                MySystem.ticks[x, y + 1] = true;
            }
        }
        if (x >= 0 && x < 10 && y - 1 >= 0 && y - 1 < 20)
        {
            if (MySystem.blocks[x, y - 1] != 0 && MySystem.blocks[x, y - 1] != 15 && MySystem.blocks[x, y -1] != 2)
            {
                MySystem.ticks[x, y - 1] = true;
            }
        }
    }

    public void Back12()
    {
        if (gameObject.GetComponent<MoveTo>() == null)
        {
            gameObject.AddComponent<MoveTo>().position = new Vector2(-1, 0);
        }
        else
        {
            GetComponent<MoveTo>().position = new Vector2(-1,0);
        }
        isBack12 = true;
        MySystem.isBack = true;
        timmer12 = 0;
        name = "god";
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
    }

    public void Back()
    {
        if (gameObject.GetComponent<MoveTo>() == null)
        {
            gameObject.AddComponent<MoveTo>().position = new Vector2(-1, 0);
        }
        else
        {
            GetComponent<MoveTo>().position = new Vector2(-1, 0);
        }
    }

    private void FixedUpdate()
    {
        if (isBack12)
        {
            timmer12++;
            if (timmer12 == 20)
            {
                int xx = 0, yy = 0;
                while (xx < 10)
                {
                    if (GameObject.Find(xx.ToString() + ',' + yy.ToString()))
                    {
                        GameObject.Find(xx.ToString() + ',' + yy.ToString()).GetComponent<Blocks>().Back();
                    }
                    yy++;
                    if (yy > 19)
                    {
                        yy = 0;
                        xx++;
                    }
                }
                Water.Reset_();
                Lava.Reset_();
            }
            if (timmer12 == 50)
            {
                int xx = 0, yy = 0;
                while (xx < 10)
                {
                    if (GameObject.Find(xx.ToString() + ',' + yy.ToString()))
                    {
                        DestroyImmediate( GameObject.Find(xx.ToString() + ',' + yy.ToString()));
                    }
                    yy++;
                    if (yy > 19)
                    {
                        yy = 0;
                        xx++;
                    }
                }
                xx = 0;
                yy = 0;
                while (xx < 10)
                {
                    MySystem.blocks[xx, yy] = before12[xx, yy];
                    yy++;
                    if (yy > 19)
                    {
                        yy = 0;
                        xx++;
                    }
                }
                xx12 = 0;
                yy12 = 0;
            }
            if (timmer12 >= 50&&xx12<10)
            {
                bool finish=true;
                while (finish)
                {
                    if (MySystem.blocks[xx12, yy12] != 0 && MySystem.blocks[xx12, yy12] != 12 && MySystem.blocks[xx12, yy12] != 13)
                    {
                        GameObject block = Instantiate(MySystem.block, new Vector3(-1, 0), Quaternion.Euler(Vector3.zero));
                        block.name = xx12.ToString() + ',' + yy12.ToString();
                        block.GetComponent<Blocks>().id = MySystem.blocks[xx12, yy12];
                        block.GetComponent<Blocks>().x = xx12;
                        block.GetComponent<Blocks>().y = yy12;
                        block.GetComponent<Blocks>()._Reset();
                        block.transform.SetParent(MySystem.BlockMother.transform);
                        finish = false;
                    }
                    xx12++;
                    if (xx12 > 9)
                    {
                        xx12 = 0;
                        yy12++;
                        if (yy12 >= 20)
                        {
                            finish = false;
                        }
                    }
                }
                
            }
            if (yy12 >= 20)
            {
                isBack12 = false;
                MySystem.isBack = false;
                id = 14;
                x = bx12;
                y = by12;
                _Reset();
                MySystem.hadGod = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "tnt" && id != 9 && id != 12 && id != 13 && id != 14 && id != 56 && id != 57 && id != 58)
        {
            if (id == 1)
            {
                id = 0;
                MySystem.score += 10;
                Instantiate(MySystem.tnt, transform.position, Quaternion.Euler(0, 0, 0));
                _Reset();
            }
            else
            {
                id = 0;
                _Reset();
            }
        }
    }
}
