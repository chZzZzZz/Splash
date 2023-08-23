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
    private GameObject MagicBullet;

    private bool startDraw = false;
    private bool isRun = false;

    private float timeScale = 1f;
    public int speed = 24;

    private GameObject ShotEffects;
    


    // Start is called before the first frame update
    void Start()
    {
       
        lineRenderer = GetComponent<LineRenderer>();
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        MagicBullet = GameObject.Find("MagicBullet");
        ShotEffects = GameObject.Find("ShotEffects");
        
        MagicBullet.transform.position = playerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startDraw)
        {
            MagicBullet.transform.position = playerPos.position;
        }
        
        DrawLineStart();
        
        DrawLineEnd();
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

            startDraw = true;
            AddPoint();
            
        }
        else if (Input.GetMouseButton(0) && startDraw)
        {
            playerPos.gameObject.SetActive(false);
            AddPoint();
        }
    }

    private void DrawLineEnd() 
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(startDraw)
            {
                isRun = true;
            }
            
            

        }
        if (isRun)
        {
            
            MagicBullet.transform.position = Vector3.MoveTowards(MagicBullet.transform.position, pointList[curIndex], speed * Time.deltaTime);
            //playerPos.position = pointList[curIndex];
            if (Vector3.Distance(MagicBullet.transform.position, pointList[curIndex]) < 0.01f)
            {
                curIndex++;
                //画特效
                GameObject ShotEffectPrefab = Resources.Load<GameObject>("Prefabs/BloodTrail");
                GameObject shotEffect = Instantiate(ShotEffectPrefab, MagicBullet.transform.position, Quaternion.identity);
                Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                Quaternion randomRotationQuaternion = Quaternion.Euler(randomRotation);
                shotEffect.transform.rotation = randomRotationQuaternion;
                shotEffect.transform.SetParent(ShotEffects.transform);

                Debug.Log(curIndex);
                //到终点
                if (curIndex == pointList.Count)
                {
                    isRun=false;
                    startDraw = false;
                    foreach (Transform child in ShotEffects.transform)
                    {
                        Animator anim = child.GetComponent<Animator>();
                        anim.SetBool("releaseBlood", true);
                    }
                    playerPos.position = pointList[curIndex - 1];
                    playerPos.gameObject.SetActive(true);
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
