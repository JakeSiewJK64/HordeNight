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
        if(player.GetComponent<PlayerPointScript>().GetPoints() >= module.GetCost() * 2 && module.GetLevel() < module.GetMaxLevel())
        {
            insufficientPointsTM.gameObject.SetActive(false);
            PlaySelectSound();
            module.Upgrade();
            player.GetComponent<UpgradeStationScript>().UpdateUpgradeItems();
            player.GetComponent<PlayerPointScript>().DeductPoints(module.GetCost());
        } else
        {
            insufficientPointsTM.gameObject.SetActive(true);
        }
        SetUpgradeModule(module);
    }

    public void SetUpgradeModule(UpgradeModule module)
    {
        this.module = module;
        insufficientPointsTM.gameObject.SetActive(player.GetComponent<PlayerPointScript>().GetPoints() < module.GetCost() * 2);
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
        if (nextLevel > module.GetMaxLevel())
        {
            nextLevelTM.text = "Max";
            valueTM.text = "";
            costTM.text = "";
            insufficientPointsTM.gameObject.SetActive(false);
        }
        else
        {
            nextLevelTM.text = ">>> Level " + nextLevel.ToString();
            valueTM.text = Math.Round(value + (value * .25f), 2) + "%";
            costTM.text = cost.ToString() + " pts";
            insufficientPointsTM.gameObject.SetActive(player.GetComponent<PlayerPointScript>().GetPoints() < module.GetCost() * 2);
        }
    }
}
