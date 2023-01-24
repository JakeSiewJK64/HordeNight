using UnityEngine;

public class ZombiesKillCounterScript : MonoBehaviour
{
    private int zombiesKilled;
    public int round;
    public int bloodmoon;
    private int maxZombies;

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
        bloodmoon = 2;
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
