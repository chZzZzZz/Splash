using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{

    public Vector2 size = new Vector2(1f, 1f);
    private LayerMask enermyLayer;

    private Vector2[] direction = {Vector2.left,Vector2.right,Vector2.up,Vector2.down};
    // Start is called before the first frame update
    void Start()
    {
        enermyLayer = LayerMask.GetMask("Enermy");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isRun)
        {
            //Í¨¹ýray
            //foreach (var d in direction)
            //{
            //    RaycastHit2D hit = Physics2D.Raycast(transform.position, d, 0.5f, enermyLayer);
            //    if (hit.collider != null)
            //    {
            //        Debug.DrawLine(transform.position, hit.point, Color.yellow);
            //        Destroy(hit.collider.gameObject);
            //    }
            //    else
            //    {
            //        Debug.DrawRay(transform.position, Vector2.right * 0.5f, Color.white);
            //    }
            //}

            Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f, enermyLayer);
            foreach(Collider2D collider in overlappingColliders) 
            {
                
                Destroy(collider.gameObject);
            }
        }
        
        
        
        
    }
}
