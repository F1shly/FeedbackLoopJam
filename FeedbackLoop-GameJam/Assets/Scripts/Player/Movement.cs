using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    //ObjsToCall
    public Inputs inputs;
    Rigidbody2D rb;

    //InputConvertion
    public float xInp;
    public Vector3 moveDirection;

    //SpeedValues
    private float trueAcceleration;
    public float acceleration = 1f;
    private float trueFriction;
    public float friction = 1f;
    public float speed;
    public float extraSpeed;
    public float maxSpeed = 1f;

    //Jumping
    public Transform jumpRayCast;
    public LayerMask mask;
    public bool canJump;
    public float jumpForce = 1f;
    public bool grounded;

    //Animations
    public Animator anim;

    //Audio
    public AudioSource playerAudio;
    public AudioClip[] FootStepArray;
    public AudioClip CurrentStepSound;
    public AudioClip JumpAudio;
    public bool delaySteps;

    //BoxTrackers
    public GameObject RunningTracker, JumpingTracker, FastFallTracker;
    public float fallingVelocityTrigger;
    public bool jumpBoxTrigger;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        ResettingGridOnAwake();
    }

    void Update()
    {
        Moving();
        Acceleration();
        Jumping();
        Audio();
        SettingBoxTracker();
        ExtraSpeed();
    }

    public void Moving()
    {
        xInp = inputs.movementInput.x;

        Vector2 movement = new Vector2(speed + extraSpeed, rb.linearVelocityY);
        rb.linearVelocity = movement;
    }
    public void ExtraSpeed()
    {
        if (extraSpeed > 0)
        {
            extraSpeed -= trueFriction * Time.deltaTime * 2;
            if (extraSpeed < 0)
                extraSpeed = 0;
        }
        else if (extraSpeed < 0)
        {

            extraSpeed += trueFriction * Time.deltaTime * 2;
            if (extraSpeed > 0)
                extraSpeed = 0;
        }
    }
    public void Acceleration()
    {
        //translatingInputs
        if (xInp > 0)
        {
            if (speed < 0)
            {
                anim.SetInteger("RunState", 3);
                speed += trueFriction * Time.deltaTime;
            }
            else
            {
                anim.SetInteger("RunState", 1);
                speed += trueAcceleration * Time.deltaTime;
                anim.transform.eulerAngles = new Vector3(anim.transform.rotation.x, 0, anim.transform.rotation.z);
            }
        }
        else if (xInp < 0)
        {
            if (speed > 0)
            {
                anim.SetInteger("RunState", 3);
                speed -= trueFriction * Time.deltaTime;
            }
            else
            {
                anim.SetInteger("RunState", 1);
                speed -= trueAcceleration * Time.deltaTime;
                anim.transform.eulerAngles = new Vector3(anim.transform.rotation.x, 180, anim.transform.rotation.z);
            }
        }
        else
        {
            if (speed > 0)
            {
                anim.SetInteger("RunState", 3);
                speed -= trueFriction * Time.deltaTime;
                if (speed < 0)
                    speed = 0;
            }
            else if (speed < 0)
            {
                anim.SetInteger("RunState", 3);
                speed += trueFriction * Time.deltaTime;
                if (speed > 0)
                    speed = 0;
            }
            else if (speed == 0)
            {
                anim.SetInteger("RunState", 0);
            }
        }
        //speedCap
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
            anim.SetInteger("RunState", 2);
        }
        if (speed <= -maxSpeed)
        {
            speed = -maxSpeed;
            anim.SetInteger("RunState", 2);
        }
    }
    public void Jumping()
    {
        //GroundCheck
        RaycastHit2D hit = Physics2D.BoxCast(jumpRayCast.position, new Vector2(0.7f, 0.1f), 0, Vector2.zero, 0.0f, mask); //jumpRayCast.TransformDirection(Vector2.down)
        if (hit.collider != null)
        {
            anim.SetBool("Grounded", true);
            canJump = true;
            trueAcceleration = acceleration;
            trueFriction = friction;
        }
        else
        {
            anim.SetBool("Grounded", false);
            canJump = false;
            jumpBoxTrigger = true;
            trueAcceleration = acceleration * 0.6f;
            trueFriction = friction * 0.6f;
        }
        //Jumping
        if (canJump && inputs.Jumping)
        {
            rb.linearVelocityY = 0;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
        }
        if (rb.linearVelocityY <= 0)
        {
            anim.SetBool("Jump", false);
        }
    }

    public void Audio()
    {
        //FOOTSTEPS
        if (canJump)
        {
            //run
            if (anim.GetInteger("RunState") == 1 &! delaySteps)
            {
                delaySteps = true;
                playerAudio.pitch = 1.7f;
                StartCoroutine(FootstepDelay(0.156f));
            }
            //sprint
            else if (anim.GetInteger("RunState") == 2 &! delaySteps)
            {
                delaySteps = true;
                playerAudio.pitch = 2f;
                StartCoroutine(FootstepDelay(0.156f));
            }
            //skid
            else if (anim.GetInteger("RunState") == 3)
            {
                //playerAudio.pitch = 2f;
                //playerAudio.volume = 0.1f;
                //playerAudio.PlayOneShot(FootStepArray[2]);
            }
            else if (anim.GetInteger("RunState") == 0)
            {
                playerAudio.Stop();
            }
        }
    }

    public void SettingBoxTracker()
    {
        if(canJump)
        {
            if (!jumpBoxTrigger)
            {
                RunningTracker.SetActive(true);
                JumpingTracker.SetActive(false);
                FastFallTracker.SetActive(false);
            }
            else
            {
                RunningTracker.SetActive(false);
                JumpingTracker.SetActive(true);
                FastFallTracker.SetActive(false);
                StartCoroutine(LandingDelay());
            }
        }
        else if(rb.linearVelocityY < fallingVelocityTrigger)
        {
            RunningTracker.SetActive(false);
            JumpingTracker.SetActive(false);
            FastFallTracker.SetActive(true);
        }
        else
        {
            RunningTracker.SetActive(false);
            JumpingTracker.SetActive(false);
            FastFallTracker.SetActive(false);
        }
    }

    IEnumerator FootstepDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CurrentStepSound = FootStepArray[Random.Range(0, 2)];
        playerAudio.PlayOneShot(CurrentStepSound);
        delaySteps = false;
    }

    IEnumerator LandingDelay()
    {
        yield return new WaitForSeconds(0.1f);
        jumpBoxTrigger = false;
    }
    
    public void OnJumpPad()
    {
        rb.linearVelocityY = 0;
        rb.AddForce(Vector2.up * jumpForce * 1.5f, ForceMode2D.Impulse);
    }

    public void ResettingGridOnAwake()
    {
        GameObject[] GridSections = GameObject.FindGameObjectsWithTag("Grid");
        foreach (var item in GridSections)
        {
            BoxCheckList checklist = item.GetComponent<BoxCheckList>();
            if (checklist.RunningTracker == null)
            { checklist.RunningTracker = RunningTracker.GetComponent<BoxCollider2D>(); }
            if (checklist.JumpingTracker == null)
            { checklist.JumpingTracker = JumpingTracker.GetComponent<BoxCollider2D>(); }
            if (checklist.FastFallTracker == null)
            { checklist.FastFallTracker = FastFallTracker.GetComponent<BoxCollider2D>(); }
        }
    }
}
