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

    private float healthbarLength = 353f;
    private float healthbarHeight = 10f;

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

    public void HideUI()
    {
        zombieHealthTM.gameObject.SetActive(false);
        zombieNameTM.gameObject.SetActive(false);
        zombieHealthbar.gameObject.SetActive(false);
        return;
    }

    private void Update()
    {
        if (zombieHealth > 0 && zombieName != null)
        {
            if (Time.time - lastUpdateTime > 5f)
            {
                HideUI();
            }

            zombieHealthTM.gameObject.SetActive(true);
            zombieNameTM.gameObject.SetActive(true);
            zombieHealthbar.gameObject.SetActive(true);

            float totalHealth = (100 + 100 * (GetComponent<ZombiesKillCounterScript>().GetRound() / GetComponent<ZombiesKillCounterScript>().GetBloodMoon()));
            zombieHealthTM.text = zombieHealth.ToString() + " / " + totalHealth;
            zombieNameTM.text = zombieName;
            zombieHealthbar.GetComponent<RectTransform>().sizeDelta = new Vector2((zombieHealth/ totalHealth) * healthbarLength, healthbarHeight);
            return;
        }
    }
}
