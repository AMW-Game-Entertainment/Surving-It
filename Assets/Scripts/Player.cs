using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [Range(0, 30f)]
    public float moveSpeed;
    [Range(0, 12f)]
    public float jumpSpeed;
    private Weapon weapon;

    // Internal Components
    private Rigidbody playerBody;

    private Vector3 keysMovementInput;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        InitPlayerWeapon();
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;


        OnMove();

        if (Input.GetButtonDown("Jump"))
            isJumping = true;
        if (Input.GetButtonDown("Attack") && weapon)
            weapon.Attck();
    }

    private void FixedUpdate()
    {
        playerBody.velocity = keysMovementInput;

        // 1. Check if is trying to jump
        // 2. Check if is on the ground
        if (isJumping && isGrounded())
        {
            OnJump();
        }
    }

    /**
     * Check player movements according AWSD
     * @return void
     */
    private void OnMove()
    {
        keysMovementInput = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, playerBody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        transform.LookAt(transform.position + new Vector3(keysMovementInput.x, 0, keysMovementInput.z));
    }

    /**
     * Force to jump the user
     * @return void
     */
    private void OnJump()
    {

        playerBody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);


        isJumping = false;
    }

    /**
     * Is the current player on the ground
     * @return bool
     */
    private bool isGrounded()
    {
        float distanceY = GetComponent<Collider>().bounds.extents.y + 0.01f;

        Ray onGround = new Ray(transform.position, Vector3.down);

        return Physics.Raycast(onGround, distanceY);
    }

    /**
     * On collision of game object call this
     * @param Colider colider Gets the references of what game object has colided with
     * @erturn void;
     */
    private void OnTriggerEnter(Collider colider)
    {
        Debug.Log(colider.tag);
        switch (colider.tag)
        {
            case "Enemy":
                if (!weapon.gameObject.activeInHierarchy)
                {
                    GameManager.instance.RestartCurrentLevel();
                }
                break;
            default:
                break;
        }
    }

    private void InitPlayerWeapon()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Weapon")
            {
                weapon = child.gameObject.GetComponent<Weapon>();
                break;
            }
        }
    }
}
