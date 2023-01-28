using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 50f;

    void Update()
    {
        if (!GetComponent<PlayerMovementScript>().rolling)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            transform.Rotate(0f, mouseX, 0f);
        }
    }
}
