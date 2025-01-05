using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthSlider;
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI LatestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [Header("Nuke")]
    [SerializeField] private TextMeshProUGUI nukeCountText;
    [SerializeField] private Slider nukeCountSlider;
    [Header("Wave")]
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI nextWaveText;
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private GameObject waveInfo;


    private void Start()
    {
        FindObjectOfType<ScoreManager>().OnScoreChanged.AddListener(UpdateScoreValue);
        
        Player playerObject = FindObjectOfType<Player>();
        
        playerObject.healthValue.OnHealthChanged.AddListener(UpdateHealthValue);
        UpdateHealthValue(playerObject.healthValue.GetHealthValue());
    }

    public GameObject WaveInfo
    {
        get { return waveInfo; }
        set { waveInfo = value; }
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
    }

    public void UpdateWaveText(int currentWave, float waveDelay, int enemiesLeft)
    {
        currentWaveText.text = currentWave.ToString();
        nextWaveText.text = waveDelay.ToString();

    }

    public void UpdateEnemiesAlive(int count)
    {
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = count.ToString();
        }
        else
        {
            Debug.LogError("enemiesalivetext is not assigned int the uimanager");
        }
    }
}
