using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(0f, 0f);
        //GetAxis�Ǵ�0�������ӵ�1���ڼ�����ֵ����Ϊ0.4 -> 0.5 -> 0.6 ...�������ӵģ�����������ٶ�Ҳ���е����Ĺ��̣�ֹ֮ͣ����л���״̬�� ��GetAxisRaw��û�����ӵĹ��̣����¾���0->1���ɿ�����1->0�������˲��������˲��ֹͣ
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        //transform.position += movement*moveSpeed*Time.deltaTime;
        //rb.velocity = movement*moveSpeed;
        Debug.Log(string.Format("{0}and{1}", rb.velocity, movement * moveSpeed));
        rb.velocity = Vector2.Lerp(rb.velocity, movement * moveSpeed, Time.deltaTime * 22f);//�ø�����ƶ�����һ�㣬�ٶ���֡���޹أ����Բ���*Time.deltaTime
        //Debug.Log(rb.velocity);
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
