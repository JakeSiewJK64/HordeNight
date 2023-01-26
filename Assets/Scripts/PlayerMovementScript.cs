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
    private float dodgeForce = 1000f;

    [SerializeField]
    private float sprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.height = 0;
        animatorController = GetComponentInChildren<Animator>();
        animatorController.SetBool("Dodgerolling", false);
    }

    private void SetIdleAnimation()
    {
        switch (GetComponent<BulletSpawnScript>().GetCurrentWeapon().weaponType)
        {
            case WeaponType.Sidearm:
                animatorController.SetBool("PistolIdle", true);
                animatorController.SetBool("RifleIdle", false);
                break;
            default:
                animatorController.SetBool("PistolIdle", false);
                animatorController.SetBool("RifleIdle", true);
                break;
        }
    }

    private void CheckSlideAnimation()
    {
        try
        {
            // check if slide animation is playing
            if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            {
                // check if slide animation is finished
                if (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    // transition to running animation
                    animatorController.Play("Idle", 0, 0.0f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && 
                Input.GetKey(KeyCode.LeftShift) && 
                !animatorController.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle") && 
                GetComponent<PlayerHealthScript>().player.stamina >= 80f)
            {
                GetComponent<PlayerHealthScript>().player.ReduceStamina(80f);
                controller.Move(transform.forward * Time.deltaTime * dodgeForce);
                animatorController.Play("Slide", 0, 0.0f);
            }
        }
        catch { }
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
        bool sprinting = false;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (!GetComponent<BuyStationScript>().interacting)
        {
            if(move != Vector3.zero) {

                animatorController.SetBool("PistolIdle", false);
                animatorController.SetBool("RifleIdle", false);
                
                
                if(shiftPressed && GetComponent<PlayerHealthScript>().player.stamina > .5f)
                {
                    GetComponent<PlayerHealthScript>().player.ReduceStamina(.5f);
                    sprinting = true;
                }  else
                {
                    sprinting = false;
                }
                
                controller.Move(move * Time.deltaTime * (sprinting? playerSpeed * sprintSpeed : playerSpeed));

                switch (GetComponent<BulletSpawnScript>().GetCurrentWeapon().weaponType)
                {
                    case WeaponType.Sidearm:
                        animatorController.SetBool("PistolWalking", true);
                        break;
                    default:
                        animatorController.SetBool("RifleWalking", true);
                        break;
                }
            } else
            {
                animatorController.SetBool("PistolWalking", false);
                animatorController.SetBool("RifleWalking", false);
                SetIdleAnimation();
            }

            CheckSlideAnimation();
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
