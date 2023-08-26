using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float offsetX = 1f;
    public float offsetY = 1f;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        if (!GameManager.Instance.startDraw)
        {
            Vector3 target = GameManager.Instance.MagicBullet.transform.position + new Vector3(offsetX, offsetY, -20f);
            
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 2000); ;
        }
        
    }
}
