using System;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuView : MonoBehaviour
{
    [SerializeField] 
    private Button playGameButton;
    [SerializeField] 
    private Button settingsButton;
    [SerializeField] 
    private Button quitButton;
    
    [SerializeField] 
    private Canvas settingsViewCanvas;
    private Canvas myCanvas;

    private void Awake()
    {
        myCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        playGameButton.onClick.AddListener(MainMenuButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void OnDestroy()
    {
        playGameButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void MainMenuButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void SettingsButtonClicked()
    {
        myCanvas.enabled = false;
        settingsViewCanvas.enabled = true;
    }
    
    private void QuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
