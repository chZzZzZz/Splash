using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyBullet : MonoBehaviour
{
    public Vector3 direction;
    private float speed = 20f;
    private GameObject enermyShotEffects;
    private float maxX=32f;
    private float maxY=12f;
    private float minX=-16f;
    private float minY=-17f;
    // Start is called before the first frame update
    void Start()
    {
        enermyShotEffects = GameObject.Find("EnermyShotEffects");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        GameObject ShotEffectPrefab = Resources.Load<GameObject>("Prefabs/BloodTrail");
        GameObject shotEffect = Instantiate(ShotEffectPrefab, transform.position, Quaternion.identity);
        Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        Quaternion randomRotationQuaternion = Quaternion.Euler(randomRotation);
        shotEffect.transform.rotation = randomRotationQuaternion;
        shotEffect.transform.localScale *= 2;
        shotEffect.transform.SetParent(enermyShotEffects.transform);
        Animator ani = shotEffect.transform.GetComponent<Animator>();
        ani.SetBool("releaseBlood", true);

        if(transform.position.x>maxX || transform.position.x<minX || transform.position.y>maxY || transform.position.y<minY)
        {
            Destroy(gameObject);
          
        }
    }
}
