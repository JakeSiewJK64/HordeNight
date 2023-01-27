using UnityEngine;

public class PlayerWeaponModelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject primaryWeaponHolder;

    [SerializeField]
    private GameObject secondaryWeaponHolder;

    private GameObject tempPrefab;

    private string basePath = "Prefabs\\WeaponPrefabs\\";

    private void UpdatePrefab(string name)
    {
        GameObject prefab = Resources.Load(basePath + name) as GameObject;
        GameObject weapon = Instantiate(prefab, 
            secondaryWeaponHolder.activeSelf ? secondaryWeaponHolder.transform.position : primaryWeaponHolder.transform.position,
            secondaryWeaponHolder.activeSelf ? secondaryWeaponHolder.transform.rotation : primaryWeaponHolder.transform.rotation
        );
        tempPrefab = weapon;
        weapon.transform.SetParent(transform);
    }

    public void UpdatePrimary(string name) 
    {
        secondaryWeaponHolder.SetActive(false);
        primaryWeaponHolder.SetActive(true);
        Destroy(tempPrefab);
        UpdatePrefab(name);
    }

    public void UpdateSecondary(string name)
    {
        secondaryWeaponHolder.SetActive(true);
        primaryWeaponHolder.SetActive(false);
        Destroy(tempPrefab);
        UpdatePrefab(name);
    }
}
