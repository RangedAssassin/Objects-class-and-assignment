using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ScoreData
{
    public string userName;
    public int highestScore;


    public ScoreData(string playerInitials, int highestScoreParameter)
    {
        this.userName = playerInitials;
        this.highestScore = highestScoreParameter;
    }
}
