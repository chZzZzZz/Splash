using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySpawner : MonoBehaviour
{

    private List<string> enermyToSpawn = new List<string>();

    private float timer = 0;
    private float start_time = 2f;
    private float time = 3;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        enermyToSpawn.Add("Prefabs/Enermy");
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>start_time && !started)
        {
            if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) > 10f)
            {
                timer = 0;
                spawn();
            }
        }

        var temp_time = time;
        if (timer > time && started)
        {
            if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) > 10f)
            {
                spawn();
            }
        }
        
    }

    private void spawn()
    {
        timer = 0;
        time -= 0.2f;
        if (time < 1)
            time = 1;

        GameObject enermyPrefab = Resources.Load<GameObject>("Prefabs/Enermy");
        Instantiate(enermyPrefab, transform.position, Quaternion.identity);
        StartCoroutine(SpawnCorotine());
    }

    private IEnumerator SpawnCorotine()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
