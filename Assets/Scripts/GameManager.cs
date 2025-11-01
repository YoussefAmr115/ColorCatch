using UnityEngine;
using UnityEngine.UI;        
using UnityEngine.SceneManagement;
using TMPro;                 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int score = 0;
    public float gameTime = 60f;
    public CollectibleColor targetColor;

    [Header("UI References")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public Image targetColorImage;
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    [Header("Audio")]
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip gameOverSound;
    public AudioClip backgroundMusic;

    private AudioSource audioSource;

    private float remainingTime;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        remainingTime = gameTime;
        PickTargetColor();
        UpdateUI();
        if (gameOverPanel) gameOverPanel.SetActive(false);

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        
        if (backgroundMusic)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = 0.3f;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (isGameOver) return;

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            remainingTime = 0;
            EndGame();
        }

        UpdateTimerText();
    }

    public void HandleCollectiblePickup(Collectible c)
    {
        if (isGameOver) return;

        if (c.color == targetColor)
        {
            score += 10;
            PlaySound(correctSound);
        }
        else
        {
            score -= 5;
            PlaySound(wrongSound);
        }

        UpdateScoreText();
    }

    void PickTargetColor()
    {
        targetColor = (CollectibleColor)Random.Range(0, 3);

        if (targetColorImage)
        {
            if (targetColor == CollectibleColor.Red)
                targetColorImage.color = Color.red;
            else if (targetColor == CollectibleColor.Blue)
                targetColorImage.color = Color.blue;
            else
                targetColorImage.color = Color.green;
        }
    }

    void UpdateUI()
    {
        UpdateScoreText();
        UpdateTimerText();
    }

    void UpdateScoreText()
    {
        if (scoreText)
            scoreText.text = "Score: " + score;
    }

    void UpdateTimerText()
    {
        if (timerText)
            timerText.text = "Time: " + Mathf.CeilToInt(remainingTime);
    }

    void EndGame()
    {
        isGameOver = true;

        PlaySound(gameOverSound);

        if (gameOverPanel)
            gameOverPanel.SetActive(true);

        if (finalScoreText)
            finalScoreText.text = "Final Score: " + score;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ApplyEnemyPenalty(float timePenalty)
    {
        if (isGameOver) return;

        remainingTime -= timePenalty;
        if (remainingTime < 0) remainingTime = 0;

        score -= 10; 
        UpdateUI();
    }

    
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

    
}
