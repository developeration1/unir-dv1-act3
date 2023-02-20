using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text scoreTextGameOver;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject gameOverScreen;

    public void UpdateAmmo(int ammo)
    {
        ammoText.text = ammo.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        scoreTextGameOver.text = score.ToString();
    }

    public void ShowGameScreen(bool show)
    {
        gameScreen.SetActive(show);
    }

    public void ShowGameOverScreen(bool show)
    {
        gameOverScreen.SetActive(show);
    }
}
