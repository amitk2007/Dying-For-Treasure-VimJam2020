using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    #region Inspector Variables
    public CharacterController controller;
    //public float playerSpeed = 40f;
    public float playerClimbingSpeed = 1f;
    public float MaxPlayerSpeed = 40f;
    private float playerSpeed;
    [SerializeField] private LadderColliderScript myLadderCollider;

    #endregion
    #region Variables
    int state = 0;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    int isJumping = 0;
    bool crouch = false;

    int jumpingCoolDown = 0;

    int isInLadder = 0;
    float gravityScale;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        collidingWith = new List<GameObject>();
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        playerSpeed = MaxPlayerSpeed;
    }

    #region Get Set
    public void SetPlayerSpeed(float input)
    {
        playerSpeed = input;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        jumpingCoolDown++;
        #region Movment Speeds
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        verticalMove = isInLadder != 0 ? Input.GetAxisRaw("Vertical") * playerClimbingSpeed : 0f;
        #endregion

        #region Keys Press
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetButtonDown("Jump") && crouch == false)
        {
            if (jumpingCoolDown > 100)
            {
                isJumping = isJumping == 0 ? 1 : isJumping;
                jump = true;
                jumpingCoolDown = 0;
            }
        }
        #endregion 

        #region Animation
        state = horizontalMove != 0 ? (int)PlayerAnimationState.walking : (int)PlayerAnimationState.idle;
        state = isJumping == 2 ? (int)PlayerAnimationState.jump : state;
        state = isInLadder != 0 ? (int)PlayerAnimationState.climbing : state;
        if (isInLadder != 0)
        {
            this.GetComponent<Animator>().speed = verticalMove == 0 ? 0 : 1;
        }
        else if (crouch == true)
        {
            state = horizontalMove == 0 ? (int)PlayerAnimationState.crouch : (int)PlayerAnimationState.crouchWalking;
        }
        else
            this.GetComponent<Animator>().speed = 1;

        if (isInLadder != 0 && verticalMove == 0)
        {
            //play idle aniamtion music
            this.GetComponent<PlayerAnimationManager>().SetAnimationStateAndSound(state, (int)PlayerAnimationState.idle);
        }
        else
        {
            this.GetComponent<PlayerAnimationManager>().SetAnimationState(state);
        }
        #endregion
    }

    //Applay the move speeds and move the player using the player controller
    private void FixedUpdate()
    {
        if (isInLadder != 0)
        {
            transform.Translate(new Vector3(0, verticalMove * Time.deltaTime, 0));
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            jump = false;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    //not jumping anymore -> stop jumping animation
    public void OnLanding()
    {
        isJumping = isJumping > 0 ? isJumping + 1 : isJumping;

        if (isJumping == 3)
        {
            isJumping = 0;
        }
    }

    private List<GameObject> collidingWith;
    //On entering the ladder
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ladder")
        {
            if (!collidingWith.Contains(collision.gameObject))
                collidingWith.Add(collision.gameObject);
            isInLadder++;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    //On exiting the ladder
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ladder")
        {
            if (collidingWith.Contains(collision.gameObject) && isInLadder == 1)
                collidingWith.Remove(collision.gameObject);
            isInLadder--;
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Ladder")
    //    {
    //        isInLadder = 0;
    //        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Ladder")
    //    {
    //        isInLadder--;
    //        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    //    }
    //}

    public bool ShouldLaddersMaterialize(GameObject ladderWhichIsChecking)
    {
        bool val = !IsHoldingDown() && !LadderColliding();
        if (collidingWith.Contains(ladderWhichIsChecking) && val == true)
        {
            isInLadder = 0;
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
        return (val);
    }

    public bool IsHoldingDown()
    {
        bool val = Input.GetAxisRaw("Vertical") == -1;
        Debug.Log("Is holding down - " + val);
        return val;
    }

    public bool LadderColliding()
    {
        bool val = myLadderCollider.IsCollidingWithLadder;
        Debug.Log("Is Colliding with ladder - " + val);
        return val;
    }
}
