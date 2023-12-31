using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineManager : MonoBehaviour
{
    
    private LineRenderer lineRenderer;

    private List<Vector2> pointList = new List<Vector2>();
    private int curIndex = 0;

    private Transform playerPos;
    private SpriteRenderer playerSprite;
    private GameObject MagicBullet;

    //private bool startDraw = false;
    //private bool isRun = false;

    private float timeScale = 1f;
    public int speed = 24;

    private GameObject ShotEffects;
    


    // Start is called before the first frame update
    void Start()
    {
       
        lineRenderer = GetComponent<LineRenderer>();
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        MagicBullet = GameObject.Find("MagicBullet");
        ShotEffects = GameObject.Find("ShotEffects");
        
        MagicBullet.transform.position = playerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlayerAlive)
        {
            if (!GameManager.Instance.isRun)
            {
                MagicBullet.transform.position = playerPos.position;
            }

            DrawLineStart();

            DrawLineEnd();
        }
    }

    private void AddPoint()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!pointList.Contains(position))
        {
            pointList.Add(position);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
        }
    }
    private void DrawLineStart()
    {
        
        float distance = Vector2.Distance(MagicBullet.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
       
        if (Input.GetMouseButtonDown(0) && distance < 2f)
        {
            timeScale = 0.001f;
            Time.timeScale = timeScale;

            GameManager.Instance.startDraw = true;
            AddPoint();
            
        }
        else if (Input.GetMouseButton(0) && GameManager.Instance.startDraw)
        {
            //playerPos.gameObject.SetActive(false);
            AddPoint();
        }
    }

    private void DrawLineEnd() 
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(GameManager.Instance.startDraw)
            {
                GameManager.Instance.isRun = true;
                lineRenderer.positionCount = 0;
                GameManager.Instance.startDraw = false;
            }
            
            

        }
        if (GameManager.Instance.isRun && !playerSprite.enabled)
        {
            MagicBullet.transform.position = Vector3.MoveTowards(MagicBullet.transform.position, pointList[curIndex], speed * Time.deltaTime);
            //playerPos.position = pointList[curIndex];
            if (Vector3.Distance(MagicBullet.transform.position, pointList[curIndex]) < 0.01f)
            {
                curIndex++;
                
                //����Ч
                GameObject ShotEffectPrefab = Resources.Load<GameObject>("Prefabs/BloodTrail");
                GameObject shotEffect = Instantiate(ShotEffectPrefab, MagicBullet.transform.position, Quaternion.identity);
                Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                Quaternion randomRotationQuaternion = Quaternion.Euler(randomRotation);
                shotEffect.transform.rotation = randomRotationQuaternion;
                shotEffect.transform.SetParent(ShotEffects.transform);

                
                //���յ�
                if (curIndex == pointList.Count)
                {
                    GameManager.Instance.isRun=false;
                    
                    foreach (Transform child in ShotEffects.transform)
                    {
                        Animator anim = child.GetComponent<Animator>();
                        anim.SetBool("releaseBlood", true);
                        Destroy(child.gameObject);
                    }
                    playerPos.position = pointList[curIndex - 1];
                    playerSprite.enabled = true;
                    
                    pointList.Clear();
                    curIndex = 0;
                    lineRenderer.positionCount = 0;
                    timeScale = 1f;
                    Time.timeScale = timeScale;
                }
            }
        }

    }
}
