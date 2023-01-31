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

    private string audioPath = "Raw\\Sound\\SoundEffects\\upgrade";

    private UpgradeModule module;

    private void Start()
    {
        insufficientPointsTM.gameObject.SetActive(true);
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
        }

        SetUpgradeModule(module);
    }

    public void SetUpgradeModule(UpgradeModule module)
    {
        this.module = module;        
        UpdateDescription();
    }

    public void UpdateUpgradeName(string name)
    {
        if(!string.IsNullOrEmpty(name))
        {
            upgradeNameTM.text = name;
        }
    }

    private void UpdateDescription()
    {
        if (module.GetLevel() + 1 > module.GetMaxLevel())
        {
            nextLevelTM.text = "Max";
            valueTM.text = "";
            costTM.text = "";
            insufficientPointsTM.gameObject.SetActive(false);
        }
        else
        {
            nextLevelTM.text = ">>> Level " + (module.GetLevel() + 1);
            valueTM.text = Math.Round(module.GetValue() + (module.GetValue() * .25f), 2) + "%";
            costTM.text = module.GetCost().ToString() + " pts";
            insufficientPointsTM.gameObject.SetActive(player.GetComponent<PlayerPointScript>().GetPoints() < (module.GetCost() * 2));
        }
    }
}
