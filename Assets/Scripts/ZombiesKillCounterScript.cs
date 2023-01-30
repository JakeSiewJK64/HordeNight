using UnityEngine;

public class ZombiesKillCounterScript : MonoBehaviour
{
    private int zombiesKilled;
    private int maxZombies;
    private int round;
    private int bloodmoon;
    private int rewardPoints;

    public int GetMaxZombies()
    {
        return maxZombies;
    }

    public void ChangeRound()
    {
        GetComponent<ZombiesKillCounterScript>().round++;
        rewardPoints = (round - 1) * 100;
        GetComponent<PlayerPointScript>().IncrementPoints(rewardPoints);
        maxZombies += GetComponent<ZombiesKillCounterScript>().round * 2;
    }

    private void Start()
    {
        bloodmoon = 7;
        ChangeRound();
    }

    public int GetBloodMoon()
    {
        if(bloodmoon == 0)
        {
            return 7;
        }
        return bloodmoon;
    }
    
    public int GetRound()
    {
        if(round== 0)
        {
            return 1;
        }
        return round;
    }

    public void IncrementCounter()
    {
        zombiesKilled++;
    }

    public void ResetCounter()
    {
        zombiesKilled = 0;
    }

    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

}
