using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    //public Animator animator;//
    
    /*[Header("Speed setup")]
    //public Vector2 velocity;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;
    public SOFloat soJumpScaleY;
    public SOFloat soJumpScaleX;
    public SOFloat soAnimationDuration;

    public Ease ease = Ease.OutBack;


    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerJump = "Jump";
    public string triggerDeath = "Death";
    public float playerSwipeDuration = .1f;*/

    private bool playerToRight = true;

    private float _currentSpeed;

    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;


    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if(collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }


    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;

        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }


    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }
        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidbody2D.MovePosition(myRigidbody2D.position - velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(-_currentSpeed, myRigidbody2D.velocity.y);
            if(myRigidbody2D.transform.localScale.x != -1)
            {
                myRigidbody2D.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            playerToRight = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidbody2D.MovePosition(myRigidbody2D.position + velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(_currentSpeed, myRigidbody2D.velocity.y);
            if(myRigidbody2D.transform.localScale.x != 1)
            {
                myRigidbody2D.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            playerToRight = true;
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }


        if(myRigidbody2D.velocity.x > 0)
        {
            myRigidbody2D.velocity += soPlayerSetup.friction;
        }
        else if (myRigidbody2D.velocity.x < 0)
        {
            myRigidbody2D.velocity -= soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidbody2D.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidbody2D.transform.localScale = new Vector2((playerToRight ? 1 : -1), 1);
            _currentPlayer.SetTrigger(soPlayerSetup.triggerJump);
            DOTween.Kill(myRigidbody2D.transform);

            HandleScaleJump();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        if (jumpVFX != null) jumpVFX.Play();
    }


    //usando scriptable object individual//
    /*private void HandleScaleJump()
    {
        myRigidbody2D.transform.DOScaleY(soJumpScaleY.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody2D.transform.DOScaleX(playerToRight ? soJumpScaleX.value : -soJumpScaleX.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }*/

    private void HandleScaleJump()
    {
        myRigidbody2D.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidbody2D.transform.DOScaleX(playerToRight ? soPlayerSetup.jumpScaleX : -soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
