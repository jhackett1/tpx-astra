using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public new Transform camera;
    public float speed = 2f;
    public float sprintSpeed = 5f;
    public float turnSpeed = 10f;
    public float animationBlendSpeed = 6f;
    public float jumpSpeed = 5f;

    float desiredAnimationSpeed;
    bool sprinting = false;
    float ySpeed = 0;
    float gravity = -9.81f;

    CharacterController controller;
    Animator animator;
    float turnSmoothVelocity;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();

        // hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // are we sprinting?
        sprinting = Input.GetKey(KeyCode.LeftShift);

        // apply gravity
        if (!controller.isGrounded)
        {
            ySpeed += gravity * Time.deltaTime;
        }
        // apply jump force
        else if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
            ySpeed += jumpSpeed;
        }
        else
        {
            ySpeed = -1;
        }

        Vector3 verticalMovement = Vector3.up * ySpeed;

        // gather normalised wasd movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(x, 0f, z).normalized;

        // orient movement in direction of camera
        Vector3 rotatedMovement = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * movement;

        // actually move the character
        controller.Move((verticalMovement + (rotatedMovement * (sprinting ? sprintSpeed : speed))) * Time.deltaTime);

        // character faces direction of movement
        if (rotatedMovement.magnitude > 0)
        {
            desiredAnimationSpeed = sprinting ? 1 : .5f;

            // calculate angle of movement vector
            float targetAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;

            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            desiredAnimationSpeed = 0;
        }

        // smoothly update animator
        animator.SetFloat("speed", Mathf.Lerp(animator.GetFloat("speed"), desiredAnimationSpeed, animationBlendSpeed * Time.deltaTime));
    }
}
