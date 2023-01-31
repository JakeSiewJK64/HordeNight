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
        insufficientPointsTM.gameObject.SetActive(false);
    }

    private void PlaySelectSound()
    {
        player.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(Path.Combine(audioPath)));
    }

    public void OnViewholderClick()
    {
        Debug.Log(player.GetComponent<PlayerPointScript>().GetPoints() == module.GetCost() * 2);
        if (player.GetComponent<PlayerPointScript>().GetPoints() >= module.GetCost() * 2 && module.GetLevel() < module.GetMaxLevel())
        {
            PlaySelectSound();
            module.Upgrade();
            player.GetComponent<UpgradeStationScript>().UpdateUpgradeItems();
            player.GetComponent<PlayerPointScript>().DeductPoints(module.GetCost());
        }
        else
        {
            insufficientPointsTM.gameObject.SetActive(true);
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
        valueTM.text = "";
        if (module.GetLevel() + 1 > module.GetMaxLevel())
        {
            nextLevelTM.text = "Max";
            costTM.text = "";
            insufficientPointsTM.gameObject.SetActive(false);
        }
        else
        {
            nextLevelTM.text = ">>> Level " + (module.GetLevel() + 1);
            costTM.text = (module.GetCost() * 2).ToString() + " pts";
            insufficientPointsTM.gameObject.SetActive(player.GetComponent<PlayerPointScript>().GetPoints() < (module.GetCost() * 2));
        }
    }
}
