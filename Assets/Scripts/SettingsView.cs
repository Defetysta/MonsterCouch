using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] 
    private Button backButton;
    [SerializeField]
    private Canvas mainMenuCanvas;
    private Canvas myCanvas;

    private void Awake()
    {
        myCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        backButton.onClick.AddListener(BackButtonClicked);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void BackButtonClicked()
    {
        myCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }
}
