using TMPro;
using UnityEngine;

public class RoundKeeperScript : MonoBehaviour
{
    public int round = 1;
    public int remainingZombies = 2;
    public TextMeshProUGUI zombieCounter;
    public TextMeshProUGUI roundCounter;

    void CountZombies()
    {
        GameObject[] allZombies = GameObject.FindGameObjectsWithTag("Zombie");
        if(allZombies.Length == 0)
        {
            round++;
            remainingZombies = round * 5;
        }
        zombieCounter.text = "Remaining Zombies: " + allZombies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        CountZombies();
        roundCounter.text = "Round: " + round;
    }
}
