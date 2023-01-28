using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerZombiehealthIndicatorScript : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI zombieHealthTM, zombieNameTM;
    
    [SerializeField] 
    private Image zombieHealthbar;
    
    private string zombieName;
    private float zombieHealth;
    private float lastUpdateTime;

    private void Start()
    {
        zombieHealthTM.gameObject.SetActive(false);
        zombieNameTM.gameObject.SetActive(false);
        zombieHealthbar.gameObject.SetActive(false);
    }

    public void SetZombie(string zombieName, float zombieHealth)
    {
        this.zombieName = zombieName;
        this.zombieHealth = zombieHealth;
        lastUpdateTime = Time.time;
    }

    private void Update()
    {
        if (zombieHealth > 0 && zombieName != null)
        {
            if (Time.time - lastUpdateTime > 5f)
            {
                zombieHealthTM.gameObject.SetActive(false);
                zombieNameTM.gameObject.SetActive(false);
                zombieHealthbar.gameObject.SetActive(false);
                return;
            }

            zombieHealthTM.gameObject.SetActive(true);
            zombieNameTM.gameObject.SetActive(true);
            zombieHealthbar.gameObject.SetActive(true);

            zombieHealthTM.text = zombieHealth.ToString() + " / " + (100 + 100 * (GetComponent<ZombiesKillCounterScript>().GetRound() / GetComponent<ZombiesKillCounterScript>().GetBloodMoon()));
            zombieNameTM.text = zombieName;
            return;
        }
    }
}
