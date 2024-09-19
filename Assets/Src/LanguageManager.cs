using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static Language language=Language.English;
    public Dropdown drop;
    public Text text;
    [TextArea]
    public List<string> strings;

    public enum Language
    {
        English=0,
        Portuguese=1,
        BahasaIndonesia=2,
        Thai=3,
        Castellano = 4,
        ChineseSimplified =5,
        Chinese=6,
    }

    public void Change()
    {
        drop.value = Mathf.Clamp(drop.value, 0, 6);
        language = (Language)drop.value;
        text.text = strings[(int)language];
        MySystem.WFileS("/language", "/language.cup", ((int)language).ToString());
    }

    private void Awake()
    {
        string s= MySystem.RFileS("/language", "/language.cup",16);
        bool hasLanguage = int.TryParse(s, out int res);

        if (hasLanguage)
        {
            if (res >= 0 && res <= 6)
            {
                language = (Language)res;

                if (drop != null)
                {
                    drop.value = res;
                }
            }
            else
            {
                language = Language.English;
                MySystem.WFileS("/language", "/language.cup", "0");
                if (drop != null)
                {
                    drop.value = 0;
                }
            }
        }
        else
        {
            language = Language.English;
            MySystem.WFileS("/language", "/language.cup", "0");
            if (drop != null)
            {
                drop.value = 0;
            }
        }
        text.text = strings[(int)language];
    }
}
