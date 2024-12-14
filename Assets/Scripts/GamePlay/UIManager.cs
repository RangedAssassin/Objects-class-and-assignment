using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI LatestScoreText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nukeCountText;
    [SerializeField] private Slider nukeCountSlider;

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
        LatestScoreText.text = score.ToString();
    }

    public void UpdateHealthValue(float health) 
    {
        healthText.text = health.ToString();
        healthSlider.value = health;
    }

    public void UpdateNukeCount(int nukeCount)
    {
        nukeCountText.text = nukeCount.ToString();
        nukeCountSlider.value = nukeCount;
        //Debug.Log("nuke count text update");
    }
}
