using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStationScript : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollArea;

    [SerializeField]
    private GameObject viewholder, upgradeDesc;

    private string audioPath = "Raw\\Sound\\SoundEffects\\menuSelect";

    private void Start()
    {
        upgradeDesc.SetActive(false);
        UpdateUpgradeItems();
    }
    public void UpdateUpgradeItems()
    {
        foreach(Transform child in scrollArea.content)
        {
            if(child != null)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (WeaponClass item in GetComponent<PlayerInventoryScript>().GetPlayerInventory().GetAllWeaponsFromInventory())
        {
            GameObject newItem = Instantiate(viewholder, scrollArea.content);
            if (newItem.TryGetComponent(out UpgradeItemViewholder viewholderItem))
            {
                viewholderItem.SetItemInfo(item);
            }
        }
    }

    private void PlaySelectSound()
    {
        upgradeDesc.SetActive(true);
        gameObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(Path.Combine(audioPath)));
    }

    public void UpdateDescriptionPanel(WeaponClass itemObject)
    {
        PlaySelectSound();
        GetComponent<UpgradeDescScript>().UpdateViewholder(itemObject);
    }
}
