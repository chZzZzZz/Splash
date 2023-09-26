using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameObject player;
    public SpriteRenderer enermySprite;
    public Transform bow;
    public SpriteRenderer bowSprite;
    public Animator enermyAnimator;
    private Rigidbody2D rb;
    public float moveSpeed = 8f;
    private bool isAttack = false;
    private bool attacking = false;
    private bool isPlayerRight;
    private GameObject enermyShotEffects;

    private Vector2 targetPlayerDir;
    private bool preshooting=false;
    private bool lockon = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        player = GameObject.Find("Player");
        enermyShotEffects = GameObject.Find("EnermyShotEffects");
        lineRenderer = GameObject.Find("LineEnermy").GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        
    }

    private void drawLine()
    {
        
    }
    void Update()
    {
        if(GameManager.Instance.isPlayerAlive)
        {
            isPlayerRight = player.transform.position.x > rb.position.x;
            enermySprite.flipX = isPlayerRight;
            //if (bowSprite) { bowSprite.flipX = isPlayerRight; }
            
        }
        
        if(preshooting)
        {
            if (!lockon)
            {
                Vector2 localPos = transform.position;
                targetPlayerDir.Normalize();

                lineRenderer.positionCount=2;
                lineRenderer.SetPosition(0,localPos);
                lineRenderer.SetPosition(1, localPos + targetPlayerDir * 100);
                float angle = Mathf.Rad2Deg * Mathf.Atan2(targetPlayerDir.y, targetPlayerDir.x);
                bow.rotation = Quaternion.Euler(0f, 0f, angle-180);
            }
            else
            {
                rb.velocity = Vector2.zero;
                Vector2 localPos = transform.position;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, localPos);
                lineRenderer.SetPosition(1, localPos + targetPlayerDir * 100);
                
                
            }
        }


        if (!isAttack)
        {
            enermyAnimator.ResetTrigger("Attack");
            enermyAnimator.SetTrigger("Walk");

        }
        if (isAttack && !attacking)
        {

            enermyAnimator.ResetTrigger("Walk");
            enermyAnimator.SetTrigger("Attack");
        }
        if (attacking)
        {
            GameObject ShotEffectPrefab = Resources.Load<GameObject>("Prefabs/BloodTrail");
            GameObject shotEffect = Instantiate(ShotEffectPrefab, transform.position, Quaternion.identity);
            Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Quaternion randomRotationQuaternion = Quaternion.Euler(randomRotation);
            shotEffect.transform.rotation = randomRotationQuaternion;
            shotEffect.transform.localScale *= 2;
            shotEffect.transform.SetParent(enermyShotEffects.transform);
            Animator ani = shotEffect.transform.GetComponent<Animator>();
            ani.SetBool("releaseBlood", true);

        }

        


    }



    #region Animation Event
    public void attack()
    {
        attacking = true;

        if (GameManager.Instance.isPlayerAlive)
        {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * 15f;
        }
        
        
        
    }
    public void attack_stop()
    {
        attacking = false;
        isAttack = false;
        foreach (Transform child in enermyShotEffects.transform)
        {
            
            Destroy(child.gameObject);

        }
    }

    public void stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void pre_shoot()
    {
        preshooting = true;
    }

    public void pre_attack()
    {
        //½ÇÉ«Ãèºì±ß
    }
    public void post_attack()
    {
        preshooting = false;
        lineRenderer.positionCount = 0;
        lockon = false;
        isAttack = false;
        foreach (Transform child in enermyShotEffects.transform)
        {

            Destroy(child.gameObject);

        }
    }

    public void lock_on()
    {
        lockon = true;
    }

    public void shoot_arrow()
    {
        Vector3 direction = targetPlayerDir * 5000;
        Vector3 cached_target_dir = targetPlayerDir;
        GameObject EnermyBulletPrefab = Resources.Load<GameObject>("Prefabs/EnermyBullet");
        GameObject enermyBullet = Instantiate(EnermyBulletPrefab, transform.position + cached_target_dir, Quaternion.identity);
        float angle = Mathf.Rad2Deg * Mathf.Atan2(targetPlayerDir.y, targetPlayerDir.x);
        enermyBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle - 180);
        EnermyBullet script = enermyBullet.GetComponent<EnermyBullet>();
        script.direction = cached_target_dir;


    }
    #endregion

    private void FixedUpdate()
    {
        if (!isAttack && GameManager.Instance.isPlayerAlive)
        {
            Vector2 direction = player.transform.position - transform.position;
            
            direction.Normalize();
            rb.velocity += direction * moveSpeed;

            rb.velocity *= Time.fixedDeltaTime * 20;

            float distance = Vector2.Distance(player.transform.position, transform.position);

            if (distance < 10f && !attacking)
            {
                if (!enermyAnimator.GetBool("Attack"))
                {
                    targetPlayerDir = direction;
                    isAttack = true;
                    rb.velocity = Vector2.zero;
                }

            }
        }

        



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.isPlayerAlive = false;
            Destroy(collision.gameObject);
        }
        

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("trigger Enter");
    //}

}
