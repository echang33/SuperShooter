using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button quitButton;
    public bool IsPaused { get; private set; }

    void Awake()
    {
        OnResumePressed();
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(OnResumePressed);
        }
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitPressed);
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (IsPaused)
            {
                OnResumePressed();
            }
            else
            {
                SetPaused(true);
            }
        }
    }

    public void OnResumePressed()
    {
        SetPaused(false);
    }

    public void OnQuitPressed()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void SetPaused(bool paused)
    {
        IsPaused = paused;
        Time.timeScale = paused ? 0f : 1f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(paused);
        }
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
    }

    void OnDestroy()
    {
        if (resumeButton != null)
        {
            resumeButton.onClick.RemoveListener(OnResumePressed);
        }
        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(OnQuitPressed);
        }
        Time.timeScale = 1f;
    }
}
