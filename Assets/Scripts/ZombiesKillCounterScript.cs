using UnityEngine;

public class ZombiesKillCounterScript : MonoBehaviour
{
    private int zombiesKilled;
    private int maxZombies;
    private int round;
    private int bloodmoon;

    public int GetMaxZombies()
    {
        return maxZombies;
    }

    public void ChangeRound()
    {
        GetComponent<ZombiesKillCounterScript>().round++;
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

    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

}
