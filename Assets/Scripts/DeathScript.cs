using UnityEngine;
using UnityEngine.SceneManagement;

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
        // manage buy station UI
        GetComponent<BuyStationScript>().CloseBuyStation();

        GetComponent<PlayerHealthScript>().player.dead = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        mainScreen.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnQuitButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
