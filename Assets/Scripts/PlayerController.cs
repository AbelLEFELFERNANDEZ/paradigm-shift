using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Ground stuff")]
    public LayerMask groundLayerMask;
    private bool isGrounded = false;
    public float coyoteTime = 0.1f;
    private float currentCoyote = 0f;
    public float groundedGraceDistance = 0.05f;

    [Header("State machine")]
    private PLAYER_STATE state = PLAYER_STATE.IDLE;

    [Header("General Movement")]
    public Rigidbody2D rBody;
    public BoxCollider2D collider;
    public SpriteRenderer renderer;

    [Header("Grounded controls")] // the "g" prefix means the variable is used when grounded
    public float g_acceleration = 25;
    public float g_drag = 14;
    public float g_MaxSpeed = 7;

    [Header("Jump")]
    public float g_jumpForce = 13;
    public float maxJumpSpeed = 13;

    [Header("Air controls")] // the "a" prefix means the variable is used when grounded
    public float a_fallingGravity = 20f;
    public float a_maxFallingSpeed = 15;
    public float a_acceleration = 6;
    public float a_MaxLateralSpeed = 3;
    public float a_LateralDrag = 2;

    [Header("DoubleJump")]
    private bool isDoubleJumpAvailable = true;
    public float a_doubleJumpForce = 4;

    private Vector3 startPosition;

    private void Awake()
    {
        if (!collider)
        {
            collider = GetComponent<BoxCollider2D>();
        }

        if (!rBody)
        {
            rBody = GetComponent<Rigidbody2D>();
        }

        if (!renderer)
        {
            renderer = GetComponent<SpriteRenderer>();
        }

        startPosition = transform.position;

    }

    private void Update()
    {

        #region countdowns
        if (currentCoyote > 0)
        {
            currentCoyote -= Time.deltaTime;
        }
        #endregion

        PlayerMovement();
        /*
        if (!renderer.isVisible)
        {
            Die();
        }*/

    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void PlayerMovement()
    {
        float inputDirection = Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("a"));
        float theoreticalYVector = 0;
        float theoreticalXVector = 0;


        #region gravity
        theoreticalYVector = rBody.velocity.y - a_fallingGravity * Time.deltaTime;
        if (theoreticalYVector < 0)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, Mathf.Sign(theoreticalYVector) * Mathf.Min(Mathf.Abs(theoreticalYVector), a_maxFallingSpeed));
        } else
        {
            rBody.velocity = new Vector2(rBody.velocity.x, theoreticalYVector);
        }
        #endregion

        #region jump

        if(Input.GetKeyDown("space") && isGrounded)
        {
            theoreticalYVector = g_jumpForce;
            rBody.velocity = new Vector2(rBody.velocity.x, Mathf.Sign(theoreticalYVector) * Mathf.Min(Mathf.Abs(theoreticalYVector), maxJumpSpeed));
            currentCoyote = 0;
            isGrounded = false;
            state = PLAYER_STATE.AIRBORNE;
        } else if (Input.GetKeyDown("space") && isDoubleJumpAvailable)
        {
            theoreticalYVector = a_doubleJumpForce;
            rBody.velocity = new Vector2(rBody.velocity.x, Mathf.Sign(theoreticalYVector) * Mathf.Min(Mathf.Abs(theoreticalYVector), maxJumpSpeed));
            isDoubleJumpAvailable = false;
        }

        #endregion

        #region groundedMovement
        if (isGrounded)
        {
            //Left and right movement
            if (inputDirection != 0)
            {
                theoreticalXVector = rBody.velocity.x + inputDirection * g_acceleration * Time.deltaTime;
                rBody.velocity = new Vector2(Mathf.Sign(theoreticalXVector) * Mathf.Min(Mathf.Abs(theoreticalXVector),g_MaxSpeed), rBody.velocity.y);
                if(state != PLAYER_STATE.GROUNDMOVING)
                {
                    state = PLAYER_STATE.GROUNDMOVING;
                }
            }
            else
            {
                theoreticalXVector = rBody.velocity.x - Mathf.Sign(rBody.velocity.x) * g_drag * Time.deltaTime;
                rBody.velocity = new Vector2(Mathf.Sign(theoreticalXVector) * Mathf.Max(0,Mathf.Sign(rBody.velocity.x) * theoreticalXVector), rBody.velocity.y);
            }
        }
        #endregion

        #region airMovement
        if (!isGrounded)
        {
            //Left and right movement
            if (inputDirection != 0)
            {
                theoreticalXVector = rBody.velocity.x + inputDirection * a_acceleration * Time.deltaTime;
                rBody.velocity = new Vector2(Mathf.Sign(theoreticalXVector) * Mathf.Min(Mathf.Abs(theoreticalXVector), a_MaxLateralSpeed), rBody.velocity.y);
            }
            else
            {
                theoreticalXVector = rBody.velocity.x - Mathf.Sign(rBody.velocity.x) * a_LateralDrag * Time.deltaTime;
                rBody.velocity = new Vector2(Mathf.Sign(theoreticalXVector) * Mathf.Max(0, Mathf.Sign(rBody.velocity.x) * theoreticalXVector), rBody.velocity.y);
            }
        }
        #endregion

    }

    private void CheckGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(collider.bounds.center.x, collider.bounds.center.y - collider.bounds.extents.y), new Vector2(collider.bounds.extents.x * 2, groundedGraceDistance), 0, Vector2.down, 0, groundLayerMask);

        if (raycastHit.collider != null && rBody.velocity.y <= 0) //Je deteste utiliser velocity pour ca, si vous avez des solutions alternatives dites-moi
        {
            isGrounded = true;
            currentCoyote = coyoteTime;
            if (state == PLAYER_STATE.AIRBORNE)
            {
                state = PLAYER_STATE.GROUNDMOVING;
                isDoubleJumpAvailable = true;
            }
        } else
        {
            if (currentCoyote <= 0)
            {
                isGrounded = false;
            }
        }

        ///See the grace distance in the scene view
        //Debug.DrawRay(collider.bounds.center, Vector2.down*(collider.bounds.extents.y + groundedGraceDistance), Color.green);
    }

    private void Die()
    {
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damaging"))
        {
            Die();
        }
    }

}

public enum PLAYER_STATE
{
    IDLE,
    GROUNDMOVING,
    AIRBORNE,
}