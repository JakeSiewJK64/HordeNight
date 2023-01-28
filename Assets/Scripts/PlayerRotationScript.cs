using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    public float sensitivity = 100f;

    void Update()
    {
        if (!GetComponent<PlayerMovementScript>().rolling)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            transform.Rotate(0f, mouseX, 0f);
        }
    }
}
