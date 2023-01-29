using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private Vector3 offset;
    
    private float smoothTime = .3f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update ()
    {
        if(target)
        {
            Vector3 targetPos = target.position + offset;
            transform.position = Vector3.SmoothDamp(
                transform.position, 
                targetPos, 
                ref velocity, smoothTime);
        }
    }
}
