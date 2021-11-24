using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    #region variables
    #region Enemy
    [Header("Enemy")]
    public Transform healthBarsParent;
    public Slider enemyHealthBarPrefab;
    #endregion

    [Header("Tower")]
    public RectTransform towerShopParent;

    #region GameStats
    [Header("Game Stats")]
    public Transform gameStatsParent;

    public TMP_Text healthText;
    public TMP_Text moneyText;
    public TMP_Text levelText;
    public TMP_Text waveText;
    public TMP_Text timeToNextWaveText;
    #endregion

    #region Control Buttons
    [Header("Control Buttons")]
    public Button pauseButton;
    public Button speedButton;
    public Button nextWaveButton;

    public TMP_Text pauseText;
    public TMP_Text speedText;
    #endregion
    #endregion

    void Awake() => Instance = this;

    void Start()
    {
        pauseButton.onClick.AddListener(PausePlay);
        speedButton.onClick.AddListener(UpdateSpeed);
        nextWaveButton.onClick.AddListener(NextWave);
    }

    void Update()
    {
        nextWaveButton.gameObject.SetActive(WaveManager.Instance.CanSkipWave);
    }

    void PausePlay()
    {
        GameManager.Instance.Paused = !GameManager.Instance.Paused;
    }

    void UpdateSpeed()
    {
        float speed = GameManager.Instance.Speed;
        speed += 1f;
        if (speed >= 4f)
            speed = 1f;
        GameManager.Instance.Speed = speed;
    }

    void NextWave()
    {
        if (WaveManager.Instance.CanSkipWave)
            WaveManager.Instance.NextWave();
    }
}