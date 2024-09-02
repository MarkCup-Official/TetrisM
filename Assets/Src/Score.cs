using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text scoreT;
    Text bestScoreT;
    int score;
    public static int bestScore;
    string scoreS;
    string bestScoreS;
    public static float beLing;

    public void _Reset()
    {
        score = 0;
    }

    private void Start()
    {
        scoreT = GameObject.Find("Score").GetComponent<Text>();
        bestScoreT = GameObject.Find("Best").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (score < MySystem.score)
        {
            score += 1;
        }
        if (score > MySystem.score)
        {
            score = MySystem.score;
        }

        bestScore = MySystem.bestScore;

        scoreS = score.ToString();
        if (scoreS.Length < 8)
        {
            int zeroCount = 8 - scoreS.Length;
            for (int i = 1; i <= zeroCount; i++)
            {
                scoreS = "0" + scoreS;
            }
        }
        if (beLing <= 0)
        {
            scoreT.text = scoreS;
        }
        else if (beLing > 0.6)
        {
            scoreT.text = "";
            beLing -= Time.deltaTime;
        }
        else if(beLing > 0.3)
        {
            scoreT.text = scoreS;
            beLing -= Time.deltaTime;
        }
        else if (beLing > 0)
        {
            scoreT.text = ""; 
            score = MySystem.score;
            beLing -= Time.deltaTime;
        }

        bestScoreS = bestScore.ToString();
        if (bestScoreS.Length < 8)
        {
            int zeroCount = 8 - bestScoreS.Length;
            for (int i = 1; i <= zeroCount; i++)
            {
                bestScoreS = "0" + bestScoreS;
            }
        }
        bestScoreT.text = bestScoreS;
    }
}
