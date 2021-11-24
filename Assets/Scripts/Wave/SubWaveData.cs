using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SubWaveData
{
    public int startTime;
    public int endTime;
    public Enemy enemy;
    public int numEnemies;

    public List<float> SpawnTimes
    {
        get
        {
            float[] spawnTimes = new float[numEnemies];

            for (int i = 0; i < numEnemies; i++)
                spawnTimes[i] = Mathf.Lerp(startTime, endTime, (float)i / numEnemies);
            return new List<float>(spawnTimes);
        }
    }
}