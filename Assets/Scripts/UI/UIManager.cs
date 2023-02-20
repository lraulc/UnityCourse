using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;
    [SerializeField] private Image livesImg;
    [SerializeField] private TMP_Text gameover_text;

    [SerializeField] private Sprite[] liveSprites;

    private void Start()
    {
        gameover_text.enabled = false;
        score_text.text = "Score: " + 0;
    }
    public void updateScoreText(int playerScore)
    {
        score_text.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        //get display image sprite
        // give it a new base on the current lives index
        livesImg.sprite = liveSprites[currentLives];
    }

    public void GameOverScreen()
    {
        gameover_text.enabled = true;
    }
}
