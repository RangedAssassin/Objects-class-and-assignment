using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button instructionsScreenButton;
    [SerializeField] private Button startScreenReturnButton;

    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject InstructionsScreen;

    [SerializeField] private TextMeshProUGUI playerIniOutput;
    [SerializeField] private TMP_InputField playersInitials;

 

    public void OnStartButtonClicked()
    {
        string initialsToTransfer = playersInitials.text;
        PlayerPrefs.SetString("playerInitials",initialsToTransfer);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }

    public void OnInstructionsButtonClicked()
    {
        StartScreen.SetActive(false);
        InstructionsScreen.SetActive(true);
    }

    public void OnReturnToStartClicked()
    {
        StartScreen.SetActive(true);
        InstructionsScreen.SetActive(false);
    }
}
