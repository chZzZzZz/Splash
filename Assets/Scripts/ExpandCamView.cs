using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExpandCamView : MonoBehaviour
{
    public float viewExtensionAmount = 1.0f;
    public float viewExtensionSpeed = 20.0f;
    
    private Camera cam;

    private Vector3 initPos;
    private float maxDistance = 3.0f;

    private float maxXDistance;
    private float maxYDistance;
    private Vector2 playerPos;

    private bool isInit = true;
    private float lastXDistanceToPlayer = 0;
    private float lastYDistanceToPlayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        maxYDistance = cam.orthographicSize;
        maxXDistance = cam.orthographicSize*cam.aspect;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.startDraw)
        {
            if (isInit)
            {
                isInit = false;
                initPos = cam.transform.position;
                Vector3 maxPos = initPos + new Vector3(maxDistance, maxDistance, 0f);

            }
            Vector2 mouseGlobalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerPos = GameManager.Instance.player.transform.position;

            float mouseY = mouseGlobalPos.y;
            float mouseX = mouseGlobalPos.x;
            //Debug.Log("mousePos:" + mouseGlobalPos);

            float diff = mouseX - playerPos.x;
            
            float viewExtentionX = 0f;
            float viewExtentionY = 0f;
            float xDelta = 0f;
            float yDelta = 0f;

            if (mouseX - playerPos.x > maxXDistance - maxDistance && mouseX - playerPos.x < maxXDistance + maxDistance && mouseX - playerPos.x > lastXDistanceToPlayer)
            {

                viewExtentionX = 1f;
                lastXDistanceToPlayer = mouseX - playerPos.x;

            }
            else if (playerPos.x - mouseX > maxXDistance - maxDistance && playerPos.x - mouseX < maxXDistance + maxDistance && playerPos.x - mouseX > lastXDistanceToPlayer)
            {
                viewExtentionX = -1f;
                lastXDistanceToPlayer = playerPos.x - mouseX;
            }

            if (mouseY - playerPos.y > maxYDistance - maxDistance && mouseY - playerPos.y < maxYDistance + maxDistance && mouseY - playerPos.y > lastYDistanceToPlayer)
            {
                viewExtentionY = 1f;
                lastYDistanceToPlayer = mouseY - playerPos.y;
            }
            else if (playerPos.y - mouseY > maxYDistance - maxDistance && playerPos.y - mouseY < maxYDistance + maxDistance && playerPos.y - mouseY > lastYDistanceToPlayer)
            {
                viewExtentionY = -1f;
                lastYDistanceToPlayer = playerPos.y - mouseY;
            }
            //Debug.Log(viewExtentionX + ", " + viewExtentionY);


            Vector3 target = cam.transform.position + new Vector3(viewExtentionX * 0.5f, viewExtentionY * 0.5f, 0f);


            target = Vector3.Lerp(cam.transform.position, target, Time.deltaTime * viewExtensionSpeed);


            cam.transform.position = target;




        }
        else
        {
            lastXDistanceToPlayer = 0f;
            lastYDistanceToPlayer = 0f;
        }


    }
}
