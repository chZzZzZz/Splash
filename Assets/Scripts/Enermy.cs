using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float moveSpeed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        rb.velocity += direction*moveSpeed;
        
        rb.velocity *= Time.fixedDeltaTime * 20;



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    GameManager.Instance.isPlayerAlive = false;
        //    Destroy(collision.gameObject);
        //}
        //Debug.Log(collision.gameObject.tag);

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("trigger Enter");
    //}






    // Update is called once per frame
    void Update()
    {
        
    }
}
