using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaScript : MonoBehaviour
{
    [SerializeField]
    private Image staminaBar;

    float speed = 2.0f;

    private void Update()
    {
        if(GetComponent<PlayerHealthScript>().player.stamina < 100)
        {
            GetComponent<PlayerHealthScript>().player.GainStamina(Time.deltaTime * 10);
        }
        staminaBar.GetComponent<RectTransform>().sizeDelta = new Vector2((GetComponent<PlayerHealthScript>().player.stamina / 100) * 250, 100);
    }
}
