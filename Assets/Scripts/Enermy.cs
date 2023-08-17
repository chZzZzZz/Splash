using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float moveSpeed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        rb.velocity += direction*moveSpeed;
        rb.velocity *= Time.deltaTime * 20;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
