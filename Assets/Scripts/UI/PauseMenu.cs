using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        resumeButton.onClick.AddListener(Resume);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitToMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pausar o jogo
    }

    public void Resume()
    {
        isPaused = false;
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f; // Retomar o jogo
    }

    private void OpenSettings()
    {
        // Implementar menu de configurações
        Debug.Log("Abrindo configurações...");
    }

    private void QuitToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}