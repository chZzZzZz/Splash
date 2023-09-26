using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;

    
    private Rigidbody2D rb;
    public SpriteRenderer playerSprite;
    public Animator playerAnimator;
    private bool lastFlip=false;
    private bool recoverFinished = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.startDraw)
        {
            rb.velocity = Vector2.zero;
        }
        //Debug.Log(rb.velocity);
        if (rb.velocity == Vector2.zero)
        {
            if (GameManager.Instance.isRun)
            {
                playerAnimator.ResetTrigger("Idle");
                playerAnimator.ResetTrigger("Recover");
                playerAnimator.ResetTrigger("Run");
                playerAnimator.SetTrigger("Shoot");
                
            }
            else
            {
                
                
                if (playerAnimator.GetBool("Shoot"))
                {
                    playerAnimator.ResetTrigger("Run");
                    playerAnimator.ResetTrigger("Idle");
                    playerAnimator.ResetTrigger("Shoot");
                    playerAnimator.SetTrigger("Recover");
                    recoverFinished = false;
                    
                }
                else
                {
                    if (recoverFinished)
                    {
                        playerAnimator.ResetTrigger("Run");
                        playerAnimator.ResetTrigger("Recover");
                        playerAnimator.ResetTrigger("Shoot");
                        playerAnimator.SetTrigger("Idle");
                    }
                }
                
            }
        }
        else
        {
            playerAnimator.ResetTrigger("Idle");
            playerAnimator.ResetTrigger("Recover");
            playerAnimator.ResetTrigger("Shoot");
            playerAnimator.SetTrigger("Run");
            
        }
    }

    public void PlayerUnVisible()
    {
        playerSprite.enabled = false;
    }

    public void RecoverFinished()
    {
        recoverFinished = true;
    }

    private void FixedUpdate()
    {
        
        Vector3 movement = new Vector3(0f, 0f);
        //GetAxis是从0缓慢增加到1，期间他的值可能为0.4 -> 0.5 -> 0.6 ...这样增加的，所以物体的速度也会有递增的过程，停止之后会有滑行状态。 而GetAxisRaw则没有增加的过程，按下就是0->1，松开就是1->0，人物会瞬间启动，瞬间停止
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (movement != Vector3.zero)
        {
            movement.Normalize();
            //transform.position += movement*moveSpeed*Time.deltaTime;
            //rb.velocity = movement*moveSpeed;
            //Debug.Log(string.Format("{0}and{1}", rb.velocity, movement * moveSpeed));
            rb.velocity = Vector2.Lerp(rb.velocity, movement * moveSpeed, Time.deltaTime * 22f);//用刚体的移动更好一点，速度与帧率无关，所以不用*Time.deltaTime
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        

        //faceclip
        if (movement.x > 0f)
        {
            playerSprite.flipX = true;
        }
        else if (movement.x < 0f)
        {
            playerSprite.flipX = false;
        }
        else
        {
            playerSprite.flipX = lastFlip;
        }
        lastFlip = playerSprite.flipX;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "wall")
        {
            Debug.Log("碰墙了");
            rb.velocity = Vector2.zero;
        }
    }
}
