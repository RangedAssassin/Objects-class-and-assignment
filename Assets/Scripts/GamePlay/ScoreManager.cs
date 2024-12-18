using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI References") ]
    [SerializeField] TextMeshProUGUI highestScoreText;
    [SerializeField] TextMeshProUGUI playerInitialsText;
    [SerializeField] TextMeshProUGUI highestInitialsText;

    
    [Header("Score Events")]
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<int> OnScoreRemoved;

    [Header("Score Data")]
    [SerializeField] private int totalScore;
    [SerializeField] private int highestScore;
    [SerializeField] private string highScoreInitials;


    [Header("Score Values")]
    [SerializeField] private int scorePerEnemy;
    [SerializeField] private int scorePerCoin;
    [SerializeField] private int scoreForPowerUp;

    [Header("Score History")]
    [SerializeField] List<ScoreData> allScores = new List<ScoreData>();
    [SerializeField] private ScoreData latestScore;

    private const string HighScoreKey = "HighScore";
    private const string PlayerInitialsKey = "playerInitials";
    private const string LatestScoreKey = "latestScore";
    private const string HighScoreInitialsKey = "HighScoreInitails";

    private void Start()
    {
        string playerInitials = PlayerPrefs.GetString(PlayerInitialsKey, "AAA");
        Debug.Log("Player Initials Retrieved: " + playerInitials);
        if (playerInitialsText != null)
        {
            playerInitialsText.text = playerInitials;
        }

        highestScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (highestScoreText != null)
        {
            highestScoreText.text = highestScore.ToString();
        }

        // Retrieve and display the initials of the high score holder
        string highScoreInitials = PlayerPrefs.GetString(HighScoreInitialsKey, "AAA");
        if (highestInitialsText != null)
        {
            highestInitialsText.text = highScoreInitials;
        }
        
        string latestScoreInJson = PlayerPrefs.GetString(LatestScoreKey, string.Empty);
        if (!string.IsNullOrEmpty(latestScoreInJson))
        {
            latestScore = JsonUtility.FromJson<ScoreData>(latestScoreInJson);
        }
        
        Player playerObject = FindObjectOfType<Player>();
        if (playerObject != null && playerObject.healthValue != null)
        {
            playerObject.healthValue.OnDied.AddListener(RegisterScore);
        }
        else
        {
            Debug.LogWarning("Player or healthValue is missing in the scene.");
        }
    }

    private void RegisterScore() // Called when the player dies
    {
        // Create an object filled with the current score information
        string playerInitials = PlayerPrefs.GetString(PlayerInitialsKey, "AAA");
        latestScore = new ScoreData(playerInitials, totalScore);

        // Convert the object into a JSON string and save it to PlayerPrefs
        string latestScoreInJson = JsonUtility.ToJson(latestScore);
        PlayerPrefs.SetString(LatestScoreKey, latestScoreInJson);

        // Check for a new high score
        if (totalScore > highestScore)
        {
            
            highestScore = totalScore;
            highScoreInitials = playerInitials;
            
            PlayerPrefs.SetInt(HighScoreKey, highestScore);
            PlayerPrefs.SetString(HighScoreInitialsKey, highScoreInitials);

            Debug.Log($"New high score: {highestScore} by {playerInitials}");
        }

        // Save changes to PlayerPrefs
        PlayerPrefs.Save();
    }
    public void IncreaseScore(ScoreType action)
    {
        switch (action)
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
