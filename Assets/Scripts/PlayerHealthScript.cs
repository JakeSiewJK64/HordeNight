using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public Player player;

    [SerializeField]
    private Image healthbar;
    
    float regenerationSpeed = .5f;
    float healthbarLength = 250f;
    float healthbarHeight = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = new Player(100, 100 , 5);
    }


    private void Update()
    {
        if (GetComponent<PlayerHealthScript>().player.health < 100)
        {
            if(GetComponent<PlayerHealthScript>().player.health <= 0)
            {
                ProcessDeath();
            }
            GetComponent<PlayerHealthScript>().player.GainHealth(Time.deltaTime * regenerationSpeed);
        } 

        healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2((GetComponent<PlayerHealthScript>().player.health / 100) * healthbarLength, healthbarHeight);
    }

    private void ProcessDeath()
    {
        gameObject.GetComponent<DeathScript>().PromptDeathScreen();
    }
}
