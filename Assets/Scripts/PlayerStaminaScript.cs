using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaScript : MonoBehaviour
{
    [SerializeField]
    private Image staminaBar;

    float regenerationSpeed = 10f;
    float staminabarLength = 250f;
    float staminabarHeight = 100f;

    private void Update()
    {
        if(GetComponent<PlayerHealthScript>().player.stamina < 100)
        {
            GetComponent<PlayerHealthScript>().player.GainStamina(Time.deltaTime * regenerationSpeed);
        }
        staminaBar.GetComponent<RectTransform>().sizeDelta = new Vector2((GetComponent<PlayerHealthScript>().player.stamina / 100) * staminabarLength, staminabarHeight);
    }
}
