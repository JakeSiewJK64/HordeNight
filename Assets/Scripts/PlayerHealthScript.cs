using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public Player player;
    public float lastDamageTime;

    [SerializeField]
    private Image healthbar;
    private float healthbarLength = 250f;
    private float healthbarHeight = 100f;
    private float healthRegenBuffer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = new Player(100, 100 , 5, 3f);
    }

    public void TakeDamage(float damage)
    {
        player.TakeDamage(damage);
        lastDamageTime = Time.time;
    }


    private void Update()
    {
        if (GetComponent<PlayerHealthScript>().player.health < 100)
        {
            // player dies
            if(GetComponent<PlayerHealthScript>().player.health <= 0)
            {
                ProcessDeath();
            } 

            if(Time.time - lastDamageTime > healthRegenBuffer) 
            {
                // regenerate health
                GetComponent<PlayerHealthScript>().player.GainHealth(Time.deltaTime * player.healthRegeneration);
            }
        } 

        healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2((GetComponent<PlayerHealthScript>().player.health / 100) * healthbarLength, healthbarHeight);
    }

    private void ProcessDeath()
    {
        gameObject.GetComponent<DeathScript>().PromptDeathScreen();
    }
}
