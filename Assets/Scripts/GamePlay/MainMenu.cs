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

    private void Start()
    {
        startGameButton.interactable = false;
        playersInitials.onValueChanged.AddListener(OnInitialsInputChanged);
    }

    private void OnInitialsInputChanged(string text)
    {
        startGameButton.interactable = !string.IsNullOrWhiteSpace(text);
    }
    //public void OnStartButtonClicked()
    //{
    //    string initialsToTransfer = playersInitials.text;
    //    string nullPlayerInitials = "AAA";
    //    if (playersInitials == null)
    //    {
    //        PlayerPrefs.SetString("playerInitials", nullPlayerInitials);
    //        PlayerPrefs.Save();
    //        SceneManager.LoadScene("GamePlay");
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetString("playerInitials", initialsToTransfer);
    //        PlayerPrefs.Save();
    //        SceneManager.LoadScene("GamePlay");
    //    }
    //}
    public void OnStartButtonClicked()
    {
        // Check if initials are provided; if not, set to "AAA"
        string initialsToTransfer = string.IsNullOrWhiteSpace(playersInitials.text) ? "AAA" : playersInitials.text;

        // Save the initials and load the gameplay scene
        PlayerPrefs.SetString("playerInitials", initialsToTransfer);
        PlayerPrefs.Save();

        // Debug message to confirm which initials were used (optional)
        Debug.Log("Player Initials: " + initialsToTransfer);

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
