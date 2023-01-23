using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private Animator animatorController;

    public CharacterController controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer = true;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    [SerializeField]
    private float sprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        animatorController = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (!GetComponent<BuyStationScript>().interacting)
        {
            if(move != Vector3.zero) {
                controller.Move(move * Time.deltaTime * (shiftPressed ? playerSpeed * sprintSpeed : playerSpeed));
                animatorController.SetBool("IsWalking", true);
            } else
            {
                animatorController.SetBool("IsWalking", false);
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
