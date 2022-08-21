using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCrosshairManager : MonoBehaviour
{
    public GameObject crosshair;
    public RaycastHit? hitInfo;
    public Vector3 scale = new Vector3(1f,1f,1f);
    
    void Update()
    {
        if (hitInfo.HasValue && hitInfo.Value.transform != null && hitInfo.Value.transform.tag.Equals("Enemy"))
        {
            foreach (var material in hitInfo.Value.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().materials)
            {
                material.shader = Shader.Find("Standard");
            }       
        }
        hitInfo = PlayerCasting.hitInfo;
        float distance = hitInfo?.distance ?? 101f;
        
        if (distance < 100 && distance > 2)
        {
            scale.Set(0.5f+1/distance,0.5f+1/distance,0.5f+1/distance);
            
        }
        else if(distance <2 )
        {
            scale.Set(1f,1f,1f);
        }
        else
        {
            scale.Set(0.5f,0.5f,0.5f);
        }
        crosshair.transform.localScale = scale;

        if (hitInfo.HasValue && hitInfo.Value.transform != null && hitInfo.Value.transform.tag.Equals("Enemy"))
        {
            crosshair.GetComponent<RawImage>().color = Color.red;
            foreach (var material in hitInfo.Value.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().materials)
            {
                material.shader = Shader.Find("Custom/Outline");
            }
        }
        else
        {
            crosshair.GetComponent<RawImage>().color = Color.white;
        }
    }
}
