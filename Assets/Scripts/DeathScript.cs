using UnityEngine;

public class DeathScript : MonoBehaviour
{
    [SerializeField]
    private Canvas mainScreen;

    [SerializeField]
    private Canvas deathScreen;

    private void Start()
    {
        deathScreen.gameObject.SetActive(false);
    }

    public void PromptDeathScreen()
    {
        mainScreen.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
