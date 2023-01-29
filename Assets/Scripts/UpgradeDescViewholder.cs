using System;
using System.IO;
using TMPro;
using UnityEngine;

public class UpgradeDescViewholder : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI upgradeNameTM, nextLevelTM, valueTM, costTM, insufficientPointsTM;
    
    [SerializeField]
    private GameObject player;

    private string audioPath = "Raw\\Sound\\SoundEffects\\menuSelect";

    private UpgradeModule module;

    private void Start()
    {
        insufficientPointsTM.gameObject.SetActive(false);
    }

    private void PlaySelectSound()
    {
        player.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(Path.Combine(audioPath)));
    }

    public void OnViewholderClick()
    {
        if(player.GetComponent<PlayerPointScript>().GetPoints() >= module.GetCost() * 2)
        {
            insufficientPointsTM.gameObject.SetActive(false);
            PlaySelectSound();
            module.Upgrade();
            SetUpgradeModule(module);
            player.GetComponent<UpgradeStationScript>().UpdateUpgradeItems();
            player.GetComponent<PlayerPointScript>().DeductPoints(module.GetCost());
        } else
        {
            insufficientPointsTM.gameObject.SetActive(true);
        }
    }

    public void SetUpgradeModule(UpgradeModule module)
    {
        this.module = module;
        UpdateDescription(nextLevel: module.GetLevel() + 1,
            value: module.GetValue() + (module.GetValue() * .25f),
            cost: module.GetCost() * 2
        );
    }

    public void UpdateUpgradeName(string name)
    {
        if(!string.IsNullOrEmpty(name))
        {
            upgradeNameTM.text = name;
        }
    }

    private void UpdateDescription(int nextLevel, double value, float cost)
    {
        nextLevelTM.text = ">>> Level " + nextLevel.ToString();
        valueTM.text = Math.Round(value + (value * .25f), 2) + "%";
        costTM.text = cost.ToString() + " pts";
        
    }
}
