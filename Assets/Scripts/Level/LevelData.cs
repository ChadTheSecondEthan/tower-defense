using UnityEngine;

[CreateAssetMenu(menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public WaveData[] waves;
    public GridData gridData;

    void OnValidate()
    {
        if (waves != null)
            foreach (WaveData waveData in waves)
            {
                if (waveData.subWaves == null || waveData.subWaves.Length == 0)
                {
                    waveData.subWaves = new SubWaveData[0];
                    continue;
                }
                int maxSpawnTime = waveData.subWaves[0].endTime;
                foreach (SubWaveData subWaveData in waveData.subWaves)
                {
                    subWaveData.startTime = Mathf.Max(subWaveData.startTime, 0);
                    subWaveData.endTime = Mathf.Max(subWaveData.startTime, subWaveData.endTime);
                    subWaveData.numEnemies = Mathf.Max(subWaveData.numEnemies, 0);
                    maxSpawnTime = Mathf.Max(maxSpawnTime, subWaveData.endTime);
                }
                waveData.nextWaveTime = Mathf.Max(maxSpawnTime, waveData.nextWaveTime);
            }
    }
}