using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField]
    private GameObject healthbar;

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
            Debug.Log("died");
            ProcessDeath();
        }
        healthbar.transform.localScale = new Vector3((player.health / 100) * 2, .125f, .125f);
    }

    private void ProcessDeath()
    {
        gameObject.GetComponent<DeathScript>().PromptDeathScreen();
    }
}
