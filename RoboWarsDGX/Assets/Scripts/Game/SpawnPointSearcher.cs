using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSearcher
{
    private GameObject[] spawnPoints;
    private Transform characterParent;
    private AreaData[] areas = new AreaData[] { new AreaData(50, 3), new AreaData(100, 2), new AreaData(150, 1) };

    public SpawnPointSearcher(GameObject[] spawnPoints, Transform characterParent)
    {
        this.spawnPoints = spawnPoints;
        this.characterParent = characterParent;
    }

    public SpawnPointSearcher(GameObject[] spawnPoints, Transform characterParent, AreaData[] areas)
    {
        this.spawnPoints = spawnPoints;
        this.characterParent = characterParent;
        this.areas = areas;
    }

    public Vector3 RandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point!");
            return Vector3.zero;
        }

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }

    public Vector3 DistanceBasedSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point!");
            return Vector3.zero;
        }
        if(characterParent.childCount == 0)
        {
            return RandomSpawnPoint();
        }

        float bestDistance = 0;
        int maxCharacter = characterParent.childCount;
        List<Vector3> points = new List<Vector3>();

        for(int i=0; i<spawnPoints.Length; i++)
        {
            float actualSmallestDistance = Mathf.Infinity;
            for(int j=0; j < maxCharacter; j++)
            {
                float distance = Vector3.Distance(spawnPoints[i].transform.position, characterParent.GetChild(j).transform.position);
                if(distance < actualSmallestDistance)
                {
                    actualSmallestDistance = distance;
                }
            }

            if(bestDistance < actualSmallestDistance)
            {
                points.Clear();
                bestDistance = actualSmallestDistance;
                points.Add(spawnPoints[i].transform.position);
            }
            else if (bestDistance == actualSmallestDistance)
            {
                points.Add(spawnPoints[i].transform.position);
            }
        }

        return points[Random.Range(0, points.Count)];
    }

    public Vector3 AreaBasedSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point!");
            return Vector3.zero;
        }

        List<Vector3> points = new List<Vector3>(); 
        float smallestScore = Mathf.Infinity;
        int maxCharacter = characterParent.childCount;
        for (int i=0; i<spawnPoints.Length; i++)
        {
            float actualScore = 0;
            for(int j = 0; j < maxCharacter; j++)
            {
                float distance = Vector3.Distance(spawnPoints[i].transform.position, characterParent.GetChild(j).transform.position);
                actualScore += AreaWeight(distance);
            }

            if(actualScore < smallestScore)
            {
                points.Clear();
                smallestScore = actualScore;
                points.Add(spawnPoints[i].transform.position);
            }
            else if(actualScore == smallestScore)
            {
                points.Add(spawnPoints[i].transform.position);
            }
        }

        return points[Random.Range(0, points.Count)];
    }

    private float AreaWeight(float distance)
    {
        for(int i=0; i<areas.Length; i++)
        {
            if(distance < areas[i].distance)
            {
                return areas[i].weight;
            }
        }
        return 0;
    }
}
