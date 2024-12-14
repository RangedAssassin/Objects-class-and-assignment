using System.Collections;
using System.Collections.Generic;
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

    public void OnStartButtonClicked()
    {
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
