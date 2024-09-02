using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


//��Ȩ����������ΪCSDN������������ɭ_����ԭ�����£���ѭCC 4.0 BY - SA��ȨЭ�飬ת���븽��ԭ�ĳ������Ӽ���������
//ԭ�����ӣ�https://blog.csdn.net/lyon_nee/article/details/77963562
public class PhoneDebug : MonoBehaviour
{
    static List<string> mLines = new List<string>();
    static List<string> mWriteTxt = new List<string>();
    private string outpath;
    void Awake()
    {
        DontDestroyOnLoad(this);
        //Application.persistentDataPath Unity��ֻ�����·���Ǽȿ��Զ�Ҳ����д�ġ�
        var filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //var filename = "log.txt";
        outpath = Path.Combine(Application.persistentDataPath, filename);
        //ÿ�������ͻ���ɾ��֮ǰ�����Log
        if (File.Exists(outpath))
        {
            File.Delete(outpath);
        }
        File.Create(outpath);
        //��������һ��Log�ļ���
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
        //��Ϊд���ļ��Ĳ������������߳�����ɣ�������Update�и���д���ļ���
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

    //�����ҰѴ������Ϣ��������������������ֻ���Ļ��
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
        bb.normal.background = null;    //�������ñ�������
        bb.normal.textColor = new Color(1, 0, 0);   //����������ɫ��
        bb.fontSize = 26;       //��Ȼ������������ɫ
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