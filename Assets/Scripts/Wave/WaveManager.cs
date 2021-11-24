using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public bool CanSkipWave { get; private set; } // have all of the enemies for this wave been spawned?

    WaveData waveData;
    List<float>[] allSpawnTimes;
    float time;
    float timeToNextWave;

    void Awake() => Instance = this;

    public void Start()
    {
        CanSkipWave = false;
        waveData = LevelManager.Instance.LevelData.waves[GameManager.Instance.Wave];
        allSpawnTimes = waveData.SpawnTimes;
        time = 0f;
        timeToNextWave = waveData.nextWaveTime - time;
        UIManager.Instance.timeToNextWaveText.SetText("--");
    }

    void Update()
    {
        if (GameManager.Instance.Paused) return;

        time += Time.deltaTime;
        timeToNextWave = waveData.nextWaveTime - time;
        UIManager.Instance.timeToNextWaveText.SetText(
            timeToNextWave < 10f && LevelManager.Instance.LevelData.waves.Length > GameManager.Instance.Wave + 1 ? 
            timeToNextWave.ToString("F") : "--");

        bool subWavesComplete = true;
        for (int i = 0; i < allSpawnTimes.Length; i++)
        {
            List<float> spawnTimes = allSpawnTimes[i];
            if (spawnTimes.Count != 0)
            {
                subWavesComplete = false;
                if (time > spawnTimes[0])
                {
                    Spawn(waveData.subWaves[i].enemy, Grid.Instance.SpawnTiles[0]);
                    spawnTimes.RemoveAt(0);
                }
            }
        }
        CanSkipWave = subWavesComplete;

        if (subWavesComplete && time > waveData.nextWaveTime)
            NextWave();
    }

    public void NextWave()
    {
        if (GameManager.Instance.Wave < LevelManager.Instance.LevelData.waves.Length - 1)
        {
            CanSkipWave = false;
            GameManager.Instance.Wave++;
            time = 0f;
            waveData = LevelManager.Instance.LevelData.waves[GameManager.Instance.Wave];
            allSpawnTimes = waveData.SpawnTimes;
        }
        else if (FindObjectsOfType<Enemy>().Length == 0)
        {
            LevelManager.Instance.NextLevel();
        }
    }

    void Spawn(Enemy e, Tile spawnTile)
    {
        Enemy enemy = Instantiate(e, spawnTile.transform);
        enemy.transform.localScale = Vector3.one * 0.6f;
    }
}