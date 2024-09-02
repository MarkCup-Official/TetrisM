using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    public static int[,] showingLava = new int[10, 320];
    public static int[,] lava = new int[10, 320];
    public static GameObject lavaKids;
    int tick = 0;
    public Vector2Int enter;

    private void FixedUpdate()
    {
        tick++;
        if (Input.GetKey("o") && Input.GetKey("p") && Input.GetKey("l"))
        {
            lava[enter.x, enter.y] = 1;
            MySystem.score = -2333333;
        }
        if (tick >= 3)
        {
            tick = 0;
            Tick();
        }
    }

    void Tick()
    {
        //0up
        int x = 0, y = 0;
        int ay = 0;
        int isLeft = System.DateTime.Now.Second;
        while (x < 10)
        {
            if ((lava[x, y] == 1 || lava[x, y] == 2) && MySystem.blocks[x, y / 16] != 0 && MySystem.blocks[x, y / 16] != 56 && MySystem.blocks[x, y / 16] != 58)
            {
                ay = 1;
                lava[x, y] = 0;
                if (isLeft % 2 == 0)
                {
                    while (true)
                    {
                        if (lava[x, y + ay] != 1 && lava[x, y + ay] != 2 && (MySystem.blocks[x, (y + ay) / 16] == 0 || MySystem.blocks[x, (y + ay) / 16] == 56 || MySystem.blocks[x, (y + ay) / 16] == 58))
                        {
                            lava[x, y + ay] = 1;
                            break;
                        }
                        if (x > 0)
                        {
                            if (lava[x - 1, y + ay] != 1 && lava[x - 1, y + ay] != 2 && (MySystem.blocks[x - 1, (y + ay) / 16] == 0 || MySystem.blocks[x - 1, (y + ay) / 16] == 56 || MySystem.blocks[x - 1, (y + ay) / 16] == 58))
                            {
                                lava[x - 1, y + ay] = 1;
                                break;
                            }
                        }
                        if (x < 9)
                        {
                            if (lava[x + 1, y + ay] != 1 && lava[x + 1, y + ay] != 2 && (MySystem.blocks[x + 1, (y + ay) / 16] == 0 || MySystem.blocks[x + 1, (y + ay) / 16] == 56 || MySystem.blocks[x + 1, (y + ay) / 16] == 58))
                            {
                                lava[x + 1, y + ay] = 1;
                                break;
                            }
                        }
                        ay++;
                        if (y + ay > 319)
                        {
                            MySystem.gameOver = true;
                            break;
                        }
                    }
                }
                else
                {
                    while (true)
                    {
                        if (lava[x, y + ay] != 1 && lava[x, y + ay] != 2 && (MySystem.blocks[x, (y + ay) / 16] == 0 || MySystem.blocks[x, (y + ay) / 16] == 56 || MySystem.blocks[x, (y + ay) / 16] == 58))
                        {
                            lava[x, y + ay] = 1;
                            break;
                        }
                        if (x < 9)
                        {
                            if (lava[x + 1, y + ay] != 1 && lava[x + 1, y + ay] != 2 && (MySystem.blocks[x + 1, (y + ay) / 16] == 0 || MySystem.blocks[x + 1, (y + ay) / 16] == 56 || MySystem.blocks[x + 1, (y + ay) / 16] == 58))
                            {
                                lava[x + 1, y + ay] = 1;
                                break;
                            }
                        }
                        if (x > 0)
                        {
                            if (lava[x - 1, y + ay] != 1 && lava[x - 1, y + ay] != 2 && (MySystem.blocks[x - 1, (y + ay) / 16] == 0 || MySystem.blocks[x - 1, (y + ay) / 16] == 56 || MySystem.blocks[x - 1, (y + ay) / 16] == 58))
                            {
                                lava[x - 1, y + ay] = 1;
                                break;
                            }
                        }
                        ay++;
                        if (y + ay > 319)
                        {
                            MySystem.gameOver = true;
                            break;
                        }
                    }
                }
            }
            isLeft++;
            y++;
            if (y >= 320)
            {
                y = 0;
                x++;
            }
        }
        //1fall
        bool fallStart = false;
        x = 0;
        y = 0;
        while (x < 10)
        {
            if (y < 319)
            {
                if ((lava[x, y + 1] == 1 || lava[x, y + 1] == 2) && lava[x, y] == 0 && (MySystem.blocks[x, y / 16] == 0 || MySystem.blocks[x, y / 16] == 56 || MySystem.blocks[x, y / 16] == 58) && fallStart == false)
                {
                    lava[x, y] = 1;
                    fallStart = true;
                }
                else if (lava[x, y + 1] == 0 && fallStart)
                {
                    lava[x, y] = 0;
                    fallStart = false;
                }
            }
            y++;
            if (y >= 320)
            {
                if (fallStart)
                {
                    lava[x, 319] = 0;
                }
                y = 0;
                x++;
            }
        }

        //2move
        x = 0;
        y = 0;
        int fallWater = -1;
        isLeft = System.DateTime.Now.Second;
        while (x < 10)
        {
            if (y > 0)
            {
                if (fallWater == -1 && lava[x, y - 1] == 0 && (lava[x, y] == 1 || lava[x, y] == 2) && (MySystem.blocks[x, (y - 1) / 16] == 0 || MySystem.blocks[x, (y - 1) / 16] == 56 || MySystem.blocks[x, (y - 1) / 16] == 58))//检测有没有水正在下落
                {
                    fallWater = y;
                }
                if (fallWater != -1 && lava[x, y] == 0)
                {
                    fallWater = -1;
                }
                if (isLeft % 2 == 1)
                {
                    if (x > 0)
                    {
                        if (fallWater == -1 && (lava[x, y] == 1 || lava[x, y] == 2) && lava[x - 1, y] != 1 && lava[x - 1, y] != 2 && (MySystem.blocks[x - 1, y / 16] == 0 || MySystem.blocks[x - 1, y / 16] == 56 || MySystem.blocks[x - 1, y / 16] == 58))
                        {
                            lava[x, y] = 0;
                            lava[x - 1, y] = 1;
                        }
                    }
                }
                else
                {
                    if (x < 9)
                    {
                        if (fallWater == -1 && (lava[x, y] == 1 || lava[x, y] == 2) && lava[x + 1, y] != 1 && lava[x + 1, y] != 2 && (MySystem.blocks[x + 1, y / 16] == 0 || MySystem.blocks[x + 1, y / 16] == 56 || MySystem.blocks[x + 1, y / 16] == 58))
                        {
                            lava[x, y] = 0;
                            lava[x + 1, y] = 1;
                        }
                    }
                }
            }
            isLeft++;
            y++;
            if (y >= 320)
            {
                y = 0;
                x++;
            }
        }

        x = 0; y = 0; int _y = 0;
        bool isBlock = true;
        while (x < 10)
        {
            if (lava[x, y * 16 + _y] == 0)
            {
                isBlock = false;
            }

            if(lava[x, y * 16 + _y] == 1 || lava[x, y * 16 + _y] == 2)
            {
                if(Water.water[x, y * 16 + _y] == 1)
                {
                    Water.DeleteWater(x, y);
                    DeleteWater(x, y);
                    MySystem.blocks[x, y] = 18;
                    MySystem.ResetBlocks();
                    MySystem.sound.PlayOneShot(MySystem.fizz);
                }
            }

            _y++;
            if (_y >= 16)
            {
                if (isBlock)
                {
                    //Debug.Log(x*1000+y);
                    MySystem.blocks[x, y] = 56;//////////////////
                    Add(x, y);
                }
                else
                {
                    if (MySystem.blocks[x, y] == 56)
                    {
                        MySystem.blocks[x, y] = 0;
                        Add(x, y);
                    }
                }
                isBlock = true;
                _y = 0;
                y++;
            }
            if (y >= 20)
            {
                y = 0;
                x++;
            }
        }

        ResetShowingLava();
    }

    public static void Reset_()
    {
        lavaKids = GameObject.Find("LavaKids");

        int x = 0, y = 0;
        while (x < 10)
        {
            lava[x, y] = 0;
            y++;
            if (y >= 320)
            {
                y = 0;
                x++;
            }
        }
        ResetShowingLava();
    }

    public static void DeleteWater(int x, int y)
    {
        int yy = 0;
        while (yy < 16)
        {
            lava[x, yy + y * 16] = 0;
            yy++;
        }
        Add(x, y);
        ResetShowingLava();
    }

    public static void AddWater(int x, int y)
    {
        int yy = 0;
        while (yy < 16)
        {
            lava[x, yy + y * 16] = 1;
            yy++;
        }
        Add(x, y);
        ResetShowingLava();
    }

    public static void ResetShowingLava()
    {
        int x = 0, y = 0;
        int enterPosition = -1;
        int lastEnterPosition = -1;
        int startPosition = -1;
        int n = 0;
        while (x < 10)
        {
            if (showingLava[x, y] == 1 && enterPosition == -1)//检测展示水
            {
                enterPosition = y;
            }
            if (showingLava[x, y] == 0 && enterPosition != -1)
            {
                lastEnterPosition = enterPosition;
                enterPosition = -1;
            }
            if (showingLava[x, y] == 1)
            {
                if (lava[x, y] != 1 && enterPosition != -1)//删除旧水
                {
                    GameObject.Find("Lava" + x.ToString() + ',' + enterPosition.ToString()).GetComponent<LavaKid>().Wei(y - enterPosition);
                }
            }
            else if (showingLava[x, y] == 0)//展示的水是空
            {
                if (lava[x, y] == 1)//此时需要展示水
                {
                    if (startPosition == -1)//记录即将增加的水头,水量增加
                    {
                        startPosition = y;
                        n += 1;
                    }
                    else
                    {
                        n += 1;
                    }
                }
                if (y < 319)
                    if ((lava[x, y + 1] == 0 || showingLava[x, y + 1] == 1) && startPosition != -1)//下一步水要结束
                    {
                        if (startPosition > 0)
                        {
                            if (showingLava[x, startPosition - 1] == 1)//旧水连着,新增旧水
                            {
                                LavaKid w = GameObject.Find("Lava" + x.ToString() + ',' + lastEnterPosition.ToString()).GetComponent<LavaKid>();
                                w.n += n;
                                w.Hello();
                                startPosition = lastEnterPosition;
                            }
                            else//没有旧水,直接新增
                            {
                                GameObject water = Instantiate(MySystem.lavaKids);
                                water.transform.SetParent(lavaKids.transform);
                                LavaKid w = water.GetComponent<LavaKid>();
                                w.n = n;
                                w.x = x;
                                w.y = startPosition;
                                w.Hello();
                                enterPosition = startPosition;
                            }
                        }
                        else//没有旧水,直接新增(防溢出)
                        {
                            GameObject water = Instantiate(MySystem.lavaKids);
                            water.transform.SetParent(lavaKids.transform);
                            LavaKid w = water.GetComponent<LavaKid>();
                            w.n = n;
                            w.x = x;
                            w.y = startPosition;
                            w.Hello();
                            enterPosition = startPosition;
                        }

                        if (y < 320)//防溢出
                        {
                            if (showingLava[x, y + 1] == 1)//顶部有水,合并
                            {
                                LavaKid w = GameObject.Find("Lava" + x.ToString() + ',' + (y + 1).ToString()).GetComponent<LavaKid>();
                                LavaKid w2 = GameObject.Find("Lava" + x.ToString() + ',' + startPosition.ToString()).GetComponent<LavaKid>();
                                w2.n += w.n;
                                w.GoodBye();
                                w2.Hello();
                                enterPosition = startPosition;
                            }
                        }
                        startPosition = -1;
                        n = 0;
                    }
            }

            y++;

            if (y >= 320)
            {
                enterPosition = -1;
                y = 0;
                x++;
            }
        }
    }

    public static void Add(int x, int y)
    {
        if (x + 1 >= 0 && x + 1 < 10 && y >= 0 && y < 20)
        {
            if (MySystem.blocks[x + 1, y] != 0)
            {
                MySystem.ticks[x + 1, y] = true;
            }
        }
        if (x - 1 >= 0 && x - 1 < 10 && y >= 0 && y < 20)
        {
            if (MySystem.blocks[x - 1, y] != 0)
            {
                MySystem.ticks[x - 1, y] = true;
            }
        }
        if (x >= 0 && x < 10 && y + 1 >= 0 && y + 1 < 20)
        {
            if (MySystem.blocks[x, y + 1] != 0)
            {
                MySystem.ticks[x, y + 1] = true;
            }
        }
        if (x >= 0 && x < 10 && y - 1 >= 0 && y - 1 < 20)
        {
            if (MySystem.blocks[x, y - 1] != 0)
            {
                MySystem.ticks[x, y - 1] = true;
            }
        }
    }

}
