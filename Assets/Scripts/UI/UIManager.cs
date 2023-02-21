using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;
    [SerializeField] private Image livesImg;
    [SerializeField] private TMP_Text gameover_text;
    [SerializeField] private TMP_Text restartLevel_text;
    [SerializeField] private Sprite[] liveSprites;

    private gamemanager game_Manager;

    private void Start()
    {
        if (gameover_text.enabled) gameover_text.enabled = false;
        if (restartLevel_text.enabled) restartLevel_text.enabled = false;
        game_Manager = GameObject.Find("Game_Manager").GetComponent<gamemanager>();
        if (game_Manager == null) { Debug.LogError("Game manager is NULL"); }
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

        if (currentLives <= 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        game_Manager.GameOver();
        // gameover_text.enabled = true;
        restartLevel_text.enabled = true;
        StartCoroutine(GameOverFlickerAnim());
    }

    IEnumerator GameOverFlickerAnim()
    {
        while (true)
        {
            gameover_text.enabled = true;
            yield return new WaitForSeconds(0.3f);
            gameover_text.enabled = false;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
