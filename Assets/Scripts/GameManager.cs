using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public GameObject player;
    public GameObject MagicBullet;
    public bool startDraw = false;
    public bool isRun = false;
    public bool isPlayerAlive = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isPlayerAlive = true;
        MagicBullet = GameObject.Find("MagicBullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
