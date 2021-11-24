using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Inspector Variables
    [SerializeField] bool paused = false;
    [SerializeField] float speed = 1f;
    [SerializeField] int health;
    [SerializeField] int money;
    [SerializeField] int level = 0;
    [SerializeField] int wave = 0;
    #endregion

    #region Getters and Setters
    public bool Paused
    {
        get => paused;
        set
        {
            paused = value;
            UIManager.Instance.pauseText.SetText(paused ? "Play" : "Pause");
        }
    }

    public float Speed
    {
        get => Time.timeScale;
        set
        {
            Time.timeScale = value;
            UIManager.Instance.speedText.SetText("Speed: x" + Time.timeScale.ToString());
        }
    }

    public int Health
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.healthText.SetText(health.ToString());
        }
    }

    public int Money
    {
        get => money;
        set
        {
            money = value;
            UIManager.Instance.moneyText.SetText(money.ToString());
        }
    }

    public int Level
    {
        get => level;
        set
        {
            level = value;
            UIManager.Instance.levelText.SetText((level + 1).ToString());
        }
    }

    public int Wave
    {
        get => wave;
        set
        {
            wave = value;
            UIManager.Instance.waveText.SetText((wave + 1).ToString());
        }
    }
    #endregion

    void Awake() => Instance = this;

    void Start()
    {
        Paused = paused;
        Speed = speed;
        Health = health;
        Money = money;
        Level = level;
        Wave = wave;
    }
}