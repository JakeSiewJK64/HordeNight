using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlescreenScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pressStart;

    [SerializeField]
    private Image start, exit;

    // Start is called before the first frame update
    void Start()
    {
        start.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            start.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            pressStart.gameObject.SetActive(false);  
        }
    }

    public void onStartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void onExitButtonClick()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
