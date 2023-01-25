using UnityEngine;

public class ZombiesKillCounterScript : MonoBehaviour
{
    private int zombiesKilled;
    private int maxZombies;

    [System.NonSerialized]
    public int round;

    [System.NonSerialized]
    public int bloodmoon;

    public int GetMaxZombies()
    {
        return maxZombies;
    }

    public void ChangeRound()
    {
        GetComponent<ZombiesKillCounterScript>().round++;
        maxZombies += GetComponent<ZombiesKillCounterScript>().round * 2;
    }

    private void Awake()
    {
        bloodmoon = 7;
        ChangeRound();
    }

    public void IncrementCounter()
    {
        zombiesKilled++;
    }

    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

}
