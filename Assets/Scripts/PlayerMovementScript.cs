using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private Animator animatorController;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer = true;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;
    private float dodgeForce = 500f;
    private float dodgeStamina = 30f;
    private float runStamina = 0.03125f;

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
                    GetComponent<PlayerHealthScript>().player.immune = false;
                    GetComponent<Collider>().isTrigger = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && 
                GetComponent<PlayerHealthScript>().player.stamina >= dodgeStamina)
            {
                GetComponent<PlayerHealthScript>().player.ReduceStamina(dodgeStamina);
                controller.Move(transform.forward * Time.deltaTime * dodgeForce);
                animatorController.Play("Slide", 0, 0.0f);
                GetComponent<PlayerHealthScript>().player.immune = true;
                    GetComponent<Collider>().isTrigger = true;
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
                
                
                if(shiftPressed && GetComponent<PlayerHealthScript>().player.stamina > runStamina)
                {
                    GetComponent<PlayerHealthScript>().player.ReduceStamina(runStamina);
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
