using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nukeCountText;

    private void Start()
    {
        FindObjectOfType<ScoreManager>().OnScoreChanged.AddListener(UpdateScoreValue);
        
        Player playerObject = FindObjectOfType<Player>();
        
        playerObject.healthValue.OnHealthChanged.AddListener(UpdateHealthValue);
        UpdateHealthValue(playerObject.healthValue.GetHealthValue());
    }
    public void UpdateScoreValue(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateHealthValue(float health) 
    {
        healthText.text = health.ToString();
    }

    public void UpdateNukeCount(int nukeCount)
    {
        nukeCountText.text = nukeCount.ToString();
        //Debug.Log("nuke count text update");
    }
}
