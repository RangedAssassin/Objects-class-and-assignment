using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent<int> OnScoreChanged;

    [SerializeField] private int totalScore;
    [SerializeField] private int highstScore;

    [Header("Score Values")]
    [SerializeField] private int scorePerEnemy;
    [SerializeField] private int scorePerCoin;
    [SerializeField] private int scoreForPowerUp;

    public void IncreaseScore(ScoreType action)
    {
        switch(action)
        {
            case ScoreType.EnemyKilled:
                totalScore += scorePerEnemy;
                break;

            case ScoreType.CoinCollected:
                totalScore += scorePerCoin;
                break;

            case ScoreType.PowerUpCollected:
                totalScore += scoreForPowerUp;
                break;

        }
        OnScoreChanged.Invoke(totalScore);
    }

    public void EnemyKilled()
    {
        //IncreaseScore(scorePerEnemy);
    }

    public void CoinCollected()
    {
        //IncreaseScore(scorePerCoin);
    }
}
