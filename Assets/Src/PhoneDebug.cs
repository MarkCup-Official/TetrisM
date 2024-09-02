using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


//版权声明：本文为CSDN博主「灬倪先森_」的原创文章，遵循CC 4.0 BY - SA版权协议，转载请附上原文出处链接及本声明。
//原文链接：https://blog.csdn.net/lyon_nee/article/details/77963562
public class PhoneDebug : MonoBehaviour
{
    static List<string> mLines = new List<string>();
    static List<string> mWriteTxt = new List<string>();
    private string outpath;
    void Awake()
    {
        DontDestroyOnLoad(this);
        //Application.persistentDataPath Unity中只有这个路径是既可以读也可以写的。
        var filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //var filename = "log.txt";
        outpath = Path.Combine(Application.persistentDataPath, filename);
        //每次启动客户端删除之前保存的Log
        if (File.Exists(outpath))
        {
            File.Delete(outpath);
        }
        File.Create(outpath);
        //在这里做一个Log的监听
        //Application.RegisterLogCallback(HandleLog);
        Application.logMessageReceived += HandleLog;
    }
    void Start()
    {
        Debug.Log(outpath);
    }

    void OnApplicationQuit()
    {
        Debug.Log("Quit");
    }
    void Update()
    {
        //因为写入文件的操作必须在主线程中完成，所以在Update中给你写入文件。
        if (mWriteTxt.Count > 0)
        {
            string[] temp = mWriteTxt.ToArray();
            using (StreamWriter writer = new StreamWriter(outpath, true, Encoding.UTF8))
            {
                foreach (string t in temp)
                {
                    writer.WriteLine(t);
                }
                mWriteTxt.Clear();
                writer.Close();
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        mWriteTxt.Add(logString);
        mWriteTxt.Add(stackTrace);
        if (type == LogType.Log)
        {
            Log(logString);
            //Log(stackTrace);
        }
    }

    //这里我把错误的信息保存起来，用来输出在手机屏幕上
    static public void Log(params object[] objs)
    {
        string text = "";
        for (int i = 0; i < objs.Length; ++i)
        {
            if (i == 0)
            {
                text += objs[i].ToString();
            }
            else
            {
                text += ", " + objs[i].ToString();
            }
        }
        if (Application.isPlaying)
        {
            if (mLines.Count > 20)
            {
                mLines.RemoveAt(0);
            }
            mLines.Add(text);

        }
    }

    void OnGUI()
    {
        GUI.color = Color.red;

        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;    //这是设置背景填充的
        bb.normal.textColor = new Color(1, 0, 0);   //设置字体颜色的
        bb.fontSize = 26;       //当然，这是字体颜色
        for (int i = 0, imax = mLines.Count; i < imax; ++i)
        {
            GUILayout.Label(mLines[i], bb);
        }


    }
    void OnDestory()
    {
        Application.logMessageReceived -= HandleLog;
    }
}