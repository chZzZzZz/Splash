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
        //GetAxis�Ǵ�0�������ӵ�1���ڼ�����ֵ����Ϊ0.4 -> 0.5 -> 0.6 ...�������ӵģ�����������ٶ�Ҳ���е����Ĺ��̣�ֹ֮ͣ����л���״̬�� ��GetAxisRaw��û�����ӵĹ��̣����¾���0->1���ɿ�����1->0�������˲��������˲��ֹͣ
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (movement != Vector3.zero)
        {
            movement.Normalize();
            //transform.position += movement*moveSpeed*Time.deltaTime;
            //rb.velocity = movement*moveSpeed;
            //Debug.Log(string.Format("{0}and{1}", rb.velocity, movement * moveSpeed));
            rb.velocity = Vector2.Lerp(rb.velocity, movement * moveSpeed, Time.deltaTime * 22f);//�ø�����ƶ�����һ�㣬�ٶ���֡���޹أ����Բ���*Time.deltaTime
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
            Debug.Log("��ǽ��");
            rb.velocity = Vector2.zero;
        }
    }
}
