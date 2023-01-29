using System;
using System.IO;
using TMPro;
using UnityEngine;

public class UpgradeDescViewholder : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI upgradeNameTM, nextLevelTM, valueTM, costTM;
    
    [SerializeField]
    private GameObject player;

    private string audioPath = "Raw\\Sound\\SoundEffects\\menuSelect";

    private UpgradeModule module;

    private void PlaySelectSound()
    {
        player.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(Path.Combine(audioPath)));
    }

    public void OnViewholderClick()
    {
        PlaySelectSound();
        module.Upgrade();
        SetUpgradeModule(module);
        player.GetComponent<UpgradeStationScript>().UpdateUpgradeItems();
    }

    public void SetUpgradeModule(UpgradeModule module)
    {
        this.module = module;
        UpdateDescription(nextLevel: module.GetLevel() + 1,
            value: module.value += (module.value * .25f),
            cost: module.cost *= 2
        );
    }

    public void UpdateUpgradeName(string name)
    {
        if(!string.IsNullOrEmpty(name))
        {
            upgradeNameTM.text = name;
        }
    }

    private void UpdateDescription(int nextLevel, float value, float cost)
    {
        nextLevelTM.text = ">>> Level " + nextLevel.ToString();
        valueTM.text = Math.Round(value + (value * .25f), 2) + "%";
        costTM.text = cost.ToString() + " pts";
    }
}
