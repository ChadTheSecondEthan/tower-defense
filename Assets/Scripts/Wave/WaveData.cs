using System;
using System.Collections.Generic;

[Serializable]
public class WaveData
{
    public SubWaveData[] subWaves;
    public int nextWaveTime;

    public List<float>[] SpawnTimes
    {
        get
        {
            List<float>[] times = new List<float>[subWaves.Length];
            for (int i = 0; i < subWaves.Length; i++)
                times[i] = subWaves[i].SpawnTimes;

            return times;
        }
    }
}