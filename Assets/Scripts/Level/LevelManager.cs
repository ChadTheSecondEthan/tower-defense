using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public LevelData LevelData { get; private set; }
    LevelData[] allLevelData;

    void Awake()
    {
        Instance = this;
        allLevelData = Resources.LoadAll<LevelData>("LevelData");
    }

    void Start()
    {
        LevelData = allLevelData[GameManager.Instance.Level];
        Grid.Instance.LoadGridData(LevelData.gridData);
    }

    public void NextLevel()
    {
        GameManager.Instance.Level++;
        GameManager.Instance.Paused = true;

        if (GameManager.Instance.Level == allLevelData.Length)
        {
            Debug.Log("Game Over!");
            return;
        }

        LevelData = allLevelData[GameManager.Instance.Level];
        Grid.Instance.DestroyAllTowers();
        Grid.Instance.LoadGridData(LevelData.gridData);

        WaveManager.Instance.Start();
    }
}
