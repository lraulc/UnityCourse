using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;
    [SerializeField] private Image livesImg;

    [SerializeField] private Sprite[] liveSprites;

    private void Start()
    {
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
}
