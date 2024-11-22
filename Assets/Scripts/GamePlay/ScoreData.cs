using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ScoreData
{
    public string userName;
    public int highestScore;


    public ScoreData(string userNameParameter, int highestScoreParameter)
    {
        userName = userNameParameter;
        highestScore = highestScoreParameter;
    }
}
