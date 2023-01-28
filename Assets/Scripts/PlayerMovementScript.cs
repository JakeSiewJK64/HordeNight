using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private Animator animatorController;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer = true;
    private float gravityValue = -9.81f;
    private float playerSpeed = 2.0f;
    private float runStamina = 0.25f;
    private float dodgeForce = 200f;
    private float dodgeStamina = 30f;
    private float lastSlideTime;

    public bool rolling = false;

    [SerializeField]
    private float sprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.height = 0;
        animatorController = GetComponentInChildren<Animator>();
        animatorController.SetBool("Dodgerolling", false);
        rolling = false;
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
        // check slide buffer
        if(Time.time - lastSlideTime > 5f)
        {
            GetComponent<PlayerHealthScript>().player.immune = false;
            GetComponent<Collider>().isTrigger = false;
        }

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
        else
        {
            rolling = false;

            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<PlayerHealthScript>().player.stamina >= dodgeStamina)
            {
                GetComponent<PlayerHealthScript>().player.ReduceStamina(dodgeStamina);
                controller.Move(transform.forward * Time.deltaTime * dodgeForce);
                animatorController.Play("Slide", 0, 0.0f);
                GetComponent<PlayerHealthScript>().player.immune = true;
                GetComponent<Collider>().isTrigger = true;
                rolling = true;
                lastSlideTime = Time.time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool sprinting = false;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 forward = Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward;
        Vector3 move = (forward * vertical) + (transform.right * horizontal);

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (!GetComponent<BuyStationScript>().interacting)
        {
            if(move != Vector3.zero) {

                animatorController.SetBool("PistolIdle", false);
                animatorController.SetBool("RifleIdle", false);
                
                if(shiftPressed && GetComponent<PlayerHealthScript>().player.stamina > 0)
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
