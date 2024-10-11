using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;

    [SerializeField] private TMP_Text coinsText;
    [SerializeField] GameObject play;
    int count=100;
    private int score = 0;
    public Button upgradeButton;

    public delegate void ScoreChangedHandler(int newScore);
    public event ScoreChangedHandler ScoreChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        coinsText.text= score.ToString();
        if (score >= 10)
        {
            upgradeButton.interactable = true;
            upgradeButton.GetComponent<Image>().color = Color.green; // Change color to indicate availability
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeButton.GetComponent<Image>().color = Color.red; // Change color to indicate unavailability
        }
        if(count==100)
        {
            play.SetActive(true);
            count = 0;
        }
    }

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.BulletHit += OnBulletHit;
        }
        else
        {
            Debug.LogWarning("GameEvents instance is not set!");
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.BulletHit -= OnBulletHit;
        }
        else
        {
            Debug.LogWarning("GameEvents instance is not set!");
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    private void OnBulletHit(Vector3 hitPosition, GameObject hitObject)
    {

        score += 10;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        coinsText.text = score.ToString();
        ScoreChanged?.Invoke(score);  // Notify listeners about score change
    }

    public bool HasEnoughScore(int requiredScore)
    {
        return score >= requiredScore;
    }

    public void SpendScore(int amount)
    {
        if (score - amount >= 0)
        {
            score -= amount;
            UpdateScoreUI();
        }
        else
        {
            Debug.LogWarning("Not enough score to spend!");
        }
    }
   public void ScoreIncrease()
    {
        count = count + 10;
        score = score + 10;
    }
}
