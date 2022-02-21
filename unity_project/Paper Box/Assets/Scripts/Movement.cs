using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float accMove;
    public float dccMove;
    public float speedWalkMax;
    public float speedRunMax;
    public float gravity;
    public float speedJump;

    [SerializeField] private AudioClip sfxStep1;
    [SerializeField] private AudioClip sfxStep2;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private float stepDistanceWalk;
    [SerializeField] private float stepDistanceRun;
    private float stepDistanceTotal;
    private bool steppedOdd;

    [HideInInspector] public bool active;

    [Header("Ref")]
    public CharacterController characterController;
    public Camera camHead;
    public CeilingChecker ceilingChecker;

    private Vector2 velocityMovement;
    private float velocityY;

    void Update()
    {
        if (active)
        {
            // get new movement input
            Vector2 inputMovement = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) { inputMovement += Vector2.up; }
            if (Input.GetKey(KeyCode.S)) { inputMovement += Vector2.down; }
            if (Input.GetKey(KeyCode.A)) { inputMovement += Vector2.left; }
            if (Input.GetKey(KeyCode.D)) { inputMovement += Vector2.right; }

            // decelerate in opposite direction
            if (velocityMovement.x < 0 && inputMovement.x >= 0 || velocityMovement.x > 0 && inputMovement.x <= 0)
            {
                velocityMovement.x = Mathf.MoveTowards(velocityMovement.x, 0, dccMove * Time.deltaTime);
            }
            if (velocityMovement.y < 0 && inputMovement.y >= 0 || velocityMovement.y > 0 && inputMovement.y <= 0)
            {
                velocityMovement.y = Mathf.MoveTowards(velocityMovement.y, 0, dccMove * Time.deltaTime);
            }

            // get move speed
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            float speedMove = isSprinting ? speedRunMax : speedWalkMax;

            // update movement velocity
            if (inputMovement.x != 0)
            {
                velocityMovement.x = Mathf.MoveTowards(velocityMovement.x, inputMovement.x * speedMove, accMove * Time.deltaTime);
            }
            if (inputMovement.y != 0)
            {
                velocityMovement.y = Mathf.MoveTowards(velocityMovement.y, inputMovement.y * speedMove, accMove * Time.deltaTime);
            }

            // clamp movement velocity
            velocityMovement = Vector3.ClampMagnitude(velocityMovement, speedMove);

            // calculate movement
            Vector3 movement = Vector3.zero;
            movement += transform.forward * velocityMovement.y;
            movement += transform.right * velocityMovement.x;

            // update gravity
            if (characterController.isGrounded)
            {
                velocityY = 0;
            }
            velocityY -= gravity * Time.deltaTime;

            // jump
            if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
            {
                velocityY = speedJump;
                AudioManager.instance.PlaySFX(sfxJump);
                stepDistanceTotal = stepDistanceWalk - 0.05f;
            }

            // kill velocity y on ceiling hit
            if (ceilingChecker.IsOn() && velocityY > 0)
            {
                velocityY = 0;
            }

            // apply movement and gravity
            characterController.Move((movement + Vector3.up * velocityY) * Time.deltaTime);

            if (characterController.isGrounded)
            {
                stepDistanceTotal += movement.magnitude * Time.deltaTime;
                float stepDistance = isSprinting ? stepDistanceRun : stepDistanceWalk;
                if (stepDistanceTotal >= stepDistance)
                {
                    stepDistanceTotal -= stepDistance;
                    AudioManager.instance.PlaySFX(steppedOdd ? sfxStep1 : sfxStep2);
                    steppedOdd = !steppedOdd;
                }
            }
        }
    }
}
