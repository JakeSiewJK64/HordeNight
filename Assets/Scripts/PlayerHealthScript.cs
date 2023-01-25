using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField]
    private Image healthbar;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = new Player(100, 100 , 5);
    }

    private void Update()
    {
        if (player.health <= 0)
        {
            ProcessDeath();
        }
        healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2((player.health / 100) * 250, 100);
    }

    private void ProcessDeath()
    {
        gameObject.GetComponent<DeathScript>().PromptDeathScreen();
    }
}
