using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static Language language=Language.English;
    public Text text;
    public List<string> strings;

    public enum Language
    {
        English=0,
        Portuguese=1,
        BahasaIndonesia=2,
        Thai=3,
        ChineseSimplified=4,
        Chinese=5,
    }

    public void Change()
    {

    }

    private void Awake()
    {
        string s= MySystem.RFileS("/language", "/language.cup",16);
        bool hasLanguage = int.TryParse(s, out int res);

        if (res >= 0 && res <= 5)
        {
            language = (Language)res;
        }
        else
        {
            language = Language.English;
            MySystem.WFileS("/language", "/language.cup", "0");
        }
    }
}
