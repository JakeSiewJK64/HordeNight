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
        healthbar.transform.localScale = new Vector3((player.health / 100) * 2, .125f, .125f);
    }

    public void ProcessDeath()
    {
        Debug.Log("you died");
    }
}
