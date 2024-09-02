using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;


public class MySystem : MonoBehaviour
{
    public static bool isPhone = true;

    static AsyncOperation scene;//加载场景用
    public static int score=0;//分数
    public static int bestScore=0;//最高分
    public static int[,] blocks = new int[10,25];//方块数据
    public static int speed=50;//下落速度
    public static Sprite[] BlocksSprite;//读取方块贴图用
    public static GameObject BlockMother,start,tnt_2,tnt,self,block,snowMan,ironMan;//方 块 之 母(储存落地方块
    public static bool isStart=false,isHold=false,gameOver=false;//游戏开始/结束状态
    public static bool[] types = new bool[7] { true, true, true, true, true, true, true };//剩余可生成方块类型
    public static bool[,] ticks = new bool[10,20];//下帧要更新的方块
    public static GameObject FallDowns , waterKids,lavaKids;//下落的方块
    public static int[] buttonDown = new int[3] { 0, 0, 0 };
    public static int[] buttonDownb = new int[3] { 0, 0, 0 };
    public static bool hadDown = false, hadTurn = false,hadGod;
    public static bool is1;
    public static bool is2,isBack=false;
    public static AudioSource sound, sound2;
    public static AudioClip 
        exp1, exp2, exp3, exp4,fuse,
        cloth1, cloth2, cloth3, cloth4,
        grass1, grass2, grass3, grass4,
        gravel1, gravel2, gravel3, gravel4,
        sand1, sand2, sand3, sand4,
        stone1, stone2, stone3, stone4,
        wood1, wood2, wood3, wood4,
        water1,water2,water3,
        lava1,lava2,lava3,
        waterFall1, waterFall2, waterFall3,
        lavaPop,lavaFall,
        glass1, glass2, glass3,
        levelup1, levelup2,
        turn1, turn2, turn3,turn4,
        fizz;

    public struct keys //按键值
    {
        public static string w = "w";
        public static string a = "a";
        public static string s = "s";
        public static string d = "d";
        public static string up = "up";
        public static string left = "left";
        public static string down = "down";
        public static string right = "right";
        public static string space = "space";
    }

    int rx,ry;

    private void FixedUpdate()
    {
        //ResetBlocks();////////////////////////////////////////////////////////////////////////
        if (!isBack)
        {
            if (blocks[rx, ry] != 0 && blocks[rx, ry] != 56 && blocks[rx, ry] != 58)
            {
                if (GameObject.Find(rx.ToString() + ',' + ry.ToString()))
                {
                    if (!(GameObject.Find(rx.ToString() + ',' + ry.ToString()).GetComponent<Blocks>().id == 15 && blocks[rx, ry] == 2))
                    {
                        GameObject.Find(rx.ToString() + ',' + ry.ToString()).GetComponent<Blocks>().id = blocks[rx, ry];
                    }
                    GameObject.Find(rx.ToString() + ',' + ry.ToString()).GetComponent<Blocks>()._Reset();
                }
                else
                {
                    GameObject bl = Instantiate(block);
                    bl.transform.position = new Vector2(-2.8f + rx * 0.4f, -3.8f + ry * 0.4f);
                    bl.transform.SetParent(BlockMother.transform);
                    Blocks b = bl.GetComponent<Blocks>();
                    b.x = rx;
                    b.y = ry;
                    b.id = blocks[rx, ry];
                    b._Reset();
                }
            }
            else
            {
                if (GameObject.Find(rx.ToString() + ',' + ry.ToString()))
                {
                    Destroy(GameObject.Find(rx.ToString() + ',' + ry.ToString()));
                }
            }

            ry++;
            if (ry >= 20)
            {
                ry = 0;
                rx++;
                if (rx >= 10)
                {
                    rx = 0;
                }
            }
        }
        
    }


    private void Start()
    {
        Reset_();
    }

    public static void ResetBlocks()
    {
        int x= 0, y = 0;
        while (x < 10)
        {
            if(blocks[x, y] != 0 && blocks[x, y] != 56 && blocks[x, y] != 58)
            {
                if(GameObject.Find(x.ToString() + ',' + y.ToString()))
                {
                    if(!(GameObject.Find(x.ToString() + ',' + y.ToString()).GetComponent<Blocks>().id==15 && blocks[x, y] == 2))
                    {
                        GameObject.Find(x.ToString() + ',' + y.ToString()).GetComponent<Blocks>().id = blocks[x, y];
                    }
                    GameObject.Find(x.ToString() + ',' + y.ToString()).GetComponent<Blocks>()._Reset();
                }
                else
                {
                    GameObject bl = Instantiate(block);
                    bl.transform.position =new Vector2(-2.8f + x * 0.4f, -3.8f + y * 0.4f);
                    bl.transform.SetParent(BlockMother.transform);
                    Blocks b = bl.GetComponent<Blocks>();
                    b.x = x;
                    b.y = y;
                    b.id = blocks[x, y];
                    b._Reset();
                }
            }
            else
            {
                if (GameObject.Find(x.ToString() + ',' + y.ToString()))
                {
                    Destroy(GameObject.Find(x.ToString() + ',' + y.ToString()));
                }
            }

            y++;
            if (y >= 20)
            {
                y = 0;
                x++;
            }
        }
    }

    private void Update()
    {
        Summon(false);
        Tick();
        if(Input.GetKey("joystick button 0"))
        {
            Debug.Log(1);
        }
        if (Input.GetKey("o")&& Input.GetKey("p"))
        {
            if (Input.GetKeyDown("r"))
            {
                Reset_();
                score = -2333333;
            }
            if (Input.GetKeyDown("s"))
            {
                isStart = true;
                score = -2333333;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (buttonDownb[0])
        {
            case 0:
                buttonDown[0] = 0;
                break;
            case 1:
                buttonDown[0] = 1;
                break;
            case 2:
                buttonDown[0] = 2;
                break;
            case 3:
                buttonDown[0] = 3;
                break;
            case 4:
                buttonDown[0] = 4;
                break;
        }
        if (buttonDownb[1]==1&&!is1)
        {
            buttonDown[1] = 1;
            is1 = true;
        }
        else if(buttonDownb[1] == 1 && is1)
        {
            buttonDown[1] = 0;
        }
        else if (buttonDownb[1] != 1 && is1)
        {
            is1 = false;
        }
        if (buttonDownb[2] == 1 && !is2)
        {
            buttonDown[2] = 1;
            is2 = true;
        }
        else if (buttonDownb[2] == 1 && is2)
        {
            buttonDown[2] = 0;
        }
        else if (buttonDownb[2] != 1 && is2)
        {
            is2 = false;
        }

        buttonDownb = new int[3] { 0, 0, 0 };

        if (!isStart)
        {
            start.SetActive(true);
            if (Input.GetKeyDown(MySystem.keys.space) || MySystem.buttonDown[2] == 1)
            {
                Summon(true);
                isStart = true;
                hadDown = true;
                start.SetActive(false);
            }
        }
        if (gameOver)
        {
            start.SetActive(true);
            Reset_();
        }
    }

    public static void Reset_()
    {
        self = GameObject.Find("mySystem");

        while (self.GetComponent<AudioSource>())
        {
            DestroyImmediate(self.GetComponent<AudioSource>());
        }

        sound = self.AddComponent<AudioSource>();
        sound2 = self.AddComponent<AudioSource>();
        sound2.volume = 0.2f;

        exp1 = Resources.Load("explode1") as AudioClip;
        exp2 = Resources.Load("explode2") as AudioClip;
        exp3 = Resources.Load("explode3") as AudioClip;
        exp4 = Resources.Load("explode4") as AudioClip;
        fuse = Resources.Load("fuse") as AudioClip;

        cloth1 = Resources.Load("cloth1") as AudioClip; 
        cloth2 = Resources.Load("cloth2") as AudioClip; 
        cloth3 = Resources.Load("cloth3") as AudioClip; 
        cloth4 = Resources.Load("cloth4") as AudioClip;

        grass1 = Resources.Load("grass1") as AudioClip;
        grass2 = Resources.Load("grass2") as AudioClip;
        grass3 = Resources.Load("grass3") as AudioClip;
        grass4 = Resources.Load("grass4") as AudioClip;

        gravel1 = Resources.Load("gravel1") as AudioClip;
        gravel2 = Resources.Load("gravel2") as AudioClip;
        gravel3 = Resources.Load("gravel3") as AudioClip;
        gravel4 = Resources.Load("gravel4") as AudioClip;

        sand1 = Resources.Load("sand1") as AudioClip;
        sand2 = Resources.Load("sand2") as AudioClip;
        sand3 = Resources.Load("sand3") as AudioClip;
        sand4 = Resources.Load("sand4") as AudioClip;

        stone1 = Resources.Load("stone1") as AudioClip;
        stone2 = Resources.Load("stone2") as AudioClip;
        stone3 = Resources.Load("stone3") as AudioClip;
        stone4 = Resources.Load("stone4") as AudioClip;

        wood1 = Resources.Load("wood1") as AudioClip;
        wood2 = Resources.Load("wood2") as AudioClip;
        wood3 = Resources.Load("wood3") as AudioClip;
        wood4 = Resources.Load("wood4") as AudioClip;

        lava1 = Resources.Load("empty_lava1") as AudioClip;
        lava2 = Resources.Load("empty_lava2") as AudioClip;
        lava3 = Resources.Load("empty_lava3") as AudioClip;
        lavaPop = Resources.Load("lavapop") as AudioClip;
        lavaFall = Resources.Load("lava") as AudioClip;

        water1 = Resources.Load("empty1") as AudioClip;
        water2 = Resources.Load("empty2") as AudioClip;
        water3 = Resources.Load("empty3") as AudioClip;
        waterFall1= Resources.Load("water1") as AudioClip;
        waterFall2 = Resources.Load("water2") as AudioClip;
        waterFall3 = Resources.Load("water3") as AudioClip;

        glass1 = Resources.Load("Glass_break1") as AudioClip;
        glass2 = Resources.Load("Glass_break2") as AudioClip;
        glass3 = Resources.Load("Glass_break3") as AudioClip;

        levelup1 = Resources.Load("levelup1") as AudioClip;
        levelup2 = Resources.Load("levelup2") as AudioClip;

        turn1 = Resources.Load("Weak_attack1") as AudioClip;
        turn2 = Resources.Load("Weak_attack2") as AudioClip;
        turn3 = Resources.Load("Weak_attack3") as AudioClip;
        turn4 = Resources.Load("Weak_attack4") as AudioClip;

        fizz= Resources.Load("Fizz") as AudioClip;

        isStart = false;
        isHold = false;
        start = GameObject.Find("start");
        gameOver = false;
        FallDowns = Resources.Load("FallDowns") as GameObject;
        block = Resources.Load("Block") as GameObject;
        tnt = Resources.Load("Tnt_1") as GameObject;
        tnt_2 = Resources.Load("Tnt_2") as GameObject;
        snowMan = Resources.Load("snowMan_1") as GameObject;
        ironMan = Resources.Load("ironMan_1") as GameObject;
        waterKids = Resources.Load("waterKids") as GameObject;
        lavaKids = Resources.Load("lavaKids") as GameObject;
        types = new bool[7] { true, true, true, true, true, true, true };
        BlocksSprite = Resources.LoadAll<Sprite>("Blocks");
        string bScore = RFileS("/Score", "/BestScore.cup", 1024);
        buttonDownb = new int[3] { 0, 0, 0 };
        buttonDown = new int[3] { 0, 0, 0 };
        Water.Reset_();
        Lava.Reset_();
        if (bScore != "error")
        {
            bestScore = int.Parse(bScore);
        }
        else
        {
            WFileS("/Score", "/BestScore.cup", "0");
        }
        if (score > bestScore)
        {
            WFileS("/Score", "/BestScore.cup", score.ToString());
            bestScore = score;
        }
        score = 0;
        GameObject.Find("Canvas").GetComponent<Score>()._Reset();

        string r = MySystem.RFileS("/key/", "key.txt", 1024);
        Physics.autoSimulation = false;
        if (r != "error")
        {
            string[] rr = r.Split(',');
            keys.w = rr[1];
            keys.a = rr[4];
            keys.s = rr[10];
            keys.d = rr[7];
            keys.up = rr[2];
            keys.left = rr[5];
            keys.down = rr[11];
            keys.right = rr[8];
            keys.space = rr[13];
        }
        else
        {
            MySystem.WFileS("/key/", "key.txt", "转向,w,up,左移,a,left,右移,d,right,下降,s,down,下落,space,\n如果要改动 请不要改变任何逗号 仅第一行有实际作用 中文仅作为提示作用 使用英文标点\n键盘代码见https://blog.csdn.net/KJHKAHDKH/article/details/116360027 删除此文件自动恢复");
        }

        int x=0;
        int y=0;
        foreach (int a in blocks)
        {
            
            blocks[x, y] = 0;
            
            y++;
            if (y > 24)
            {
                y = 0;
                x++;
                if (x > 9)
                {
                    break;
                }
            }
        }
        x = 0;
        y = 0;
        foreach (bool a in ticks)
        {
            ticks[x, y] = false;

            y++;
            if (y > 19)
            {
                y = 0;
                x++;
                if (x > 9)
                {
                    break;
                }
            }
        }
        while (GameObject.Find("Showing"))
        {
            DestroyImmediate(GameObject.Find("Showing"));
        }
        while (GameObject.Find("Tnt_1"))
        {
            DestroyImmediate(GameObject.Find("Tnt_1"));
        }
        while (GameObject.Find("FallDowns"))
        {
            DestroyImmediate(GameObject.Find("FallDowns"));
        } 
        while (GameObject.Find("BlockMother"))
        {
            DestroyImmediate(GameObject.Find("BlockMother"));
        }
        BlockMother = new GameObject();
        BlockMother.name = "BlockMother";
    }

    public static void ButtonDown(int inp)
    {
        switch (inp)
        {
            case 1:
                buttonDownb[0] = 1;
                break;
            case 2:
                buttonDownb[0] = 2;
                break;
            case 3:
                buttonDownb[0] = 3;
                break;
            case 4:
                buttonDownb[0] = 4;
                break;
            case 5:
                buttonDownb[1] = 1;
                break;
            case 6:
                buttonDownb[2] = 1;
                break;
        }
    }
    
    public static void Summon(bool isFirst)
    {
        if (isStart && !isHold)
        {
            GameObject.Find("Showing").GetComponent<FallDown>().isShow = false;
            GameObject.Find("Showing").GetComponent<FallDown>().EndShow();
            int typeNumber = 0;
            foreach (bool t in types)
            {
                if (t)
                {
                    typeNumber++;
                }
            }
            if (typeNumber == 0)
            {
                types = new bool[7] { true, true, true, true, true, true, true };
            }
            typeNumber = UnityEngine.Random.Range(0, 7);
            while (!types[typeNumber])
            {
                typeNumber = UnityEngine.Random.Range(0, 7);
            }
            types[typeNumber] = false;
            GameObject fall= Instantiate( FallDowns);
            fall.GetComponent<FallDown>().type = typeNumber;
            
        }else if (isFirst)
        {
            int typeNumber = 0;
            foreach (bool t in types)
            {
                if (t)
                {
                    typeNumber++;
                }
            }
            if (typeNumber == 0)
            {
                types = new bool[7] { true, true, true, true, true, true, true };
            }
            typeNumber = UnityEngine.Random.Range(0, 7);
            while (!types[typeNumber])
            {
                typeNumber = UnityEngine.Random.Range(0, 7);
            }
            types[typeNumber] = false;
            GameObject fall = Instantiate(FallDowns);
            fall.GetComponent<FallDown>().type = typeNumber;
            fall.GetComponent<FallDown>().isShow = false;
            fall.GetComponent<FallDown>().EndShow();
            typeNumber = 0;
            foreach (bool t in types)
            {
                if (t)
                {
                    typeNumber++;
                }
            }
            if (typeNumber == 0)
            {
                types = new bool[7] { true, true, true, true, true, true, true };
            }
            typeNumber = UnityEngine.Random.Range(0, 7);
            while (!types[typeNumber])
            {
                typeNumber = UnityEngine.Random.Range(0, 7);
            }
            types[typeNumber] = false;
            fall = Instantiate(FallDowns);
            fall.GetComponent<FallDown>().type = typeNumber;
        }
    }


    public static void Tick()
    {
        bool check = false;
        int x = 0;
        int y = 0;
        while(y<20)
        {
            if (ticks[x, y]&&!isBack)
            {
                BGMM.FakeUpdate();
                check = true;
                if (blocks[x, y] != 0 && blocks[x, y] != 56 && blocks[x, y] != 58)
                    GameObject.Find(x.ToString() + ',' + y.ToString()).GetComponent<Blocks>().Tick();
            }
            ticks[x, y] = false;

            x++;
            if (x > 9)
            {
                x = 0;
                y++;
            }
        }
        if (check)
        {
            Check();
        }
    }

    public static void Check()
    {
        int x = 0;
        int y = 0;
        int total = 0;
        bool[] t = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        foreach (int a in blocks)
        {
            if (blocks[x, y] != 0)
            {
                total++;
            }
            x++;
            if (x > 9)
            {
                if (total == 10)
                {
                    t[y] = true;
                }
                total = 0;
                x = 0;
                y++;
                if (y > 19)
                {
                    break;
                }
            }
        }

        total = 0;
        y = 0;
        foreach(bool a in t)
        {
            x = 0;
            if (a)
            {
                total++;
                while (x < 10)//xiao chu
                {
                    if (blocks[x, y]!=13)
                    {
                        if(blocks[x, y] == 56)
                        {
                            Lava.DeleteWater(x, y);
                        }
                        else if (blocks[x, y] == 58)
                        {
                            Water.DeleteWater(x, y);
                        }
                        else
                        {
                            Destroy(GameObject.Find(x.ToString() + ',' + y.ToString()));
                        }
                    }
                    else
                    {
                        if(!isBack)
                        GameObject.Find(x.ToString() + ',' + y.ToString()).GetComponent<Blocks>().Back12();
                    }
                    blocks[x, y] = 0;
                    x++;
                }
            }
            else if(total>0)
            {
                FallAbove(y - 1, total);
                if (total > 2)
                {
                    sound.PlayOneShot(levelup2);
                }
                else
                {
                    sound.PlayOneShot(levelup1);
                }
                score += (int)Mathf.Pow(2, total) * 100;
                Score.beLing = 0.9f;
                total = 0;
            }
            y++;
        }
        if (total > 0)
        {
            FallAbove(0, total);
            score += (int)Mathf.Pow(2,total) * 100;
            Score.beLing = 0.9f;
        }
    }

    public static void FallAbove(int y,int total)
    {
        GameObject.Find("Main Camera").GetComponent<MyCamera>().Shake(0, 0.4f);
        int _x;
        int _y = 0;
        
        while (y + _y < 20)
        {
            _y++;
            _x = 0;
            while (_x < 10)
            {
                if (blocks[_x, y+_y] != 0&& blocks[_x, y + _y] != 56&& blocks[_x, y + _y] != 58)
                {
                    GameObject.Find(_x.ToString() + ',' + (y + _y).ToString()).GetComponent<Blocks>().Fall(y,total);
                } 
                _x++;
            }
        }
        ResetBlocks();
    }

    public static void StartLoadScene(string name)
    {
        scene = SceneManager.LoadSceneAsync(name);
        scene.allowSceneActivation = false;
    }

    public static void LoadScene()
    {
        scene.allowSceneActivation = true;
    }

    public static void WFileS(string folder, string file, string inof)//写文件(单一字符串覆盖)WFileS("/Score", "/BestScore.cup", "0");
    {
        if (!Directory.Exists(Application.persistentDataPath + folder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + folder);
        }
        if (!File.Exists(Application.persistentDataPath + folder + file))
        {
            FileStream c = File.Create(Application.persistentDataPath + folder + file);
            c.Close();
            c.Dispose();
        }

        byte[] b = Encoding.UTF8.GetBytes(inof);

        FileStream f = File.OpenWrite(Application.persistentDataPath + folder + file);
        f.Write(b, 0, b.Length);
        f.Close();
        f.Dispose();
    }

    public static string RFileS(string folder, string file, int length)//读文件(UTF-8编码 整个文件为一个字符串)RFileS("/score", "/HiScore.cup", 1024)
    {
        if (!Directory.Exists(Application.persistentDataPath + folder))
        {
            return "error";
        }
        if (!File.Exists(Application.persistentDataPath + folder + file))
        {
            return "error";
        }

        FileStream f = File.OpenRead(Application.persistentDataPath + folder + file);
        byte[] buffer = new byte[length + 1];
        f.Read(buffer, 0, length);
        f.Close();
        f.Dispose();

        string r = Encoding.UTF8.GetString(buffer);

        return r;
    }

}
