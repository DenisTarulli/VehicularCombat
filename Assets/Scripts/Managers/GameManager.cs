using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject gameplayUI;
    [HideInInspector] public bool gameIsOver;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(Instance);
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void GameOver()
    {
        gameIsOver = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;

        gameplayUI.SetActive(false);
        gameOverUI.SetActive(true);

        if (FindObjectOfType<PlayerCombat>().CurrentHealth <= 0f)
            loseText.SetActive(true);
        else
            winText.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
