using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelTileList")]
public  class EntityDatabase :ScriptableObject
{
    public  GameObject[] minorEnemys;
    public  GameObject[] bosses;
    public  GameObject[] powerUps;

    public GameObject GETRandomEnemy()
    {
        return minorEnemys[Random.Range(0, minorEnemys.Length)];
    }

    public GameObject GETRandomBoss()
    {
        return bosses[Random.Range(0, bosses.Length)];
    }
    public GameObject GETRandomPowerUp()
    {
        return powerUps[Random.Range(0, powerUps.Length)];
    }

}
