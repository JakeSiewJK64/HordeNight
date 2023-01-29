using System.IO;
using TMPro;
using UnityEngine;

public class UpgradeDescViewholder : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI upgradeName, nextLevel, value, cost;
    
    [SerializeField]
    private GameObject player;
    
    private string audioPath = "Raw\\Sound\\SoundEffects\\menuSelect";

    private void PlaySelectSound()
    {
        player.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(Path.Combine(audioPath)));
    }

    public void OnViewholderClick()
    {
        PlaySelectSound();
    }

    public void UpdateDescription(string upgradeName, int nextLevel, float value, float cost)
    {
        this.upgradeName.text = upgradeName;
        this.nextLevel.text = ">>> Level " + nextLevel.ToString();
        this.value.text = value + (value * .25f) + "%";
        this.cost.text = cost.ToString() + " pts";
    }
}
