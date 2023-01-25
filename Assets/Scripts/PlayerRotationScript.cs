using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    public float sensitivity = 100f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        transform.Rotate(0f, mouseX, 0f);
    }
}
