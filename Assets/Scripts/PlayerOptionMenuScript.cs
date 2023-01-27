using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOptionMenuScript : MonoBehaviour
{
    [SerializeField]
    private Canvas optionMenu;

    void Start()
    {
        optionMenu.gameObject.SetActive(false);
    }

    public void onExitButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gameObject.GetComponent<BuyStationScript>().interacting && !GetComponent<PlayerHealthScript>().player.dead)
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            optionMenu.gameObject.SetActive(!optionMenu.gameObject.activeSelf);
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
}
