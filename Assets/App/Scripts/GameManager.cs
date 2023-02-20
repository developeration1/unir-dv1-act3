using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    private UIManager uiManager;
    private PlayerController player;
    public int Score { get; }

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                GameManager[] search = FindObjectsOfType<GameManager>();
                if (search == null || search.Length == 0)
                {
                    Debug.LogError("There is no Game Manager.");
                    return null;
                }
                else
                {
                    instance = search[0];
                }
            }
            return instance;
        }
    }

    private GameState State
    {
        set
        {
            switch (value)
            {
                case GameState.None:
                    SceneManager.LoadScene(0);
                    break;
                case GameState.Game:
                    uiManager.ShowGameOverScreen(false);
                    uiManager.ShowGameScreen(true);
                    player.CanMove = true; ;
                    break;
                case GameState.GameOver:
                    uiManager.ShowGameOverScreen(true);
                    uiManager.ShowGameScreen(false);
                    player.CanMove = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        if(uiManager == null)
            Debug.LogError("No UIManager found.");
        player = FindObjectOfType<PlayerController>();
        if (player == null)
            Debug.LogError("No PlayerController found.");
        State = GameState.Game;
    }

    public void AddScore(int score)
    {
        this.score += score;
        uiManager.UpdateScore(this.score);
    }

    public void CheckPlayerAmmo(int currentAmmo)
    {
        uiManager.UpdateAmmo(currentAmmo);
        if (currentAmmo <= 0)
        {
            State = GameState.GameOver;
        }
    }

    public void RestartGame()
    {
        State = GameState.None;
    }
}

public enum GameState
{
    None,
    Game,
    GameOver
}