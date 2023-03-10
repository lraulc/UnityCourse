using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthFill;
    [SerializeField] private TMP_Text gameover_text;
    [SerializeField] private TMP_Text restartLevel_text;
    [SerializeField] private TMP_Text finalScore_text;
    [SerializeField] private Sprite[] liveSprites;

    private GameManager game_Manager;


    Material healthMaterial;
    [SerializeField] private Color warningColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField] private Color dangerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    Color startingHealthColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    Color lineColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);


    int healthShaderID;
    int lineShaderID;

    private void Awake()
    {
        healthMaterial = healthFill.material;
        healthShaderID = Shader.PropertyToID("_HealthColor");
        lineShaderID = Shader.PropertyToID("_LineColor");
    }

    private void Start()
    {
        if (gameover_text.enabled) gameover_text.enabled = false;
        if (restartLevel_text.enabled) restartLevel_text.enabled = false;
        game_Manager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (game_Manager == null) { Debug.LogError("Game manager is NULL"); }
        score_text.text = "Score: " + 0;
        if (finalScore_text.enabled) finalScore_text.enabled = false;
        finalScore_text.text = score_text.text;

        // Set Starting Health Color
        startingHealthColor = healthMaterial.GetColor(healthShaderID);
        healthMaterial.SetColor(healthShaderID, startingHealthColor);
        // Set Starting Line Color
        lineColor = healthMaterial.GetColor(lineShaderID);
        healthMaterial.SetColor(lineShaderID, lineColor);
    }

    public void updateScoreText(int playerScore)
    {
        score_text.text = "Score: " + playerScore.ToString();
        finalScore_text.text = score_text.text;

    }

    public void UpdateLineColor(bool isShieldActive)
    {
        if (isShieldActive == true)
        {
            healthMaterial.SetColor(lineShaderID, Color.blue);
        }
        else
        {
            healthMaterial.SetColor(lineShaderID, Color.green);
        }
    }

    public void UpdateLives(int currentLives)
    {
        //get display image sprite
        // give it a new base on the current lives index
        // livesImg.sprite = liveSprites[currentLives];

        healthSlider.value = currentLives;

        if (currentLives >= healthSlider.maxValue)
        {
            healthMaterial.SetColor(healthShaderID, startingHealthColor);
        }

        if (currentLives == 2)
        {
            healthMaterial.SetColor(healthShaderID, warningColor);
        }

        if (currentLives == 1)
        {
            healthMaterial.SetColor(healthShaderID, dangerColor);
        }

        if (currentLives <= 0)
        {
            healthMaterial.SetColor(healthShaderID, startingHealthColor);
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        game_Manager.GameOver();
        finalScore_text.enabled = true;
        restartLevel_text.enabled = true;
        StartCoroutine(GameOverFlickerAnim());
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
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
