using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{    
    [SerializeField] Transform target;    // Takip edilecek nesne
    [SerializeField] Vector3 offset;  // Ne kadar uzakl�ktan  takip edecek

    [SerializeField] float lerpValue;
    void LateUpdate()
    {
        if(PlayerManager.instance.levelState == PlayerManager.LevelState.NotFinished)
        {
            Vector3 desPos = target.position + offset;  // Kamera ile takip edilen obje aras�ndaki mesafe
            transform.position = Vector3.Lerp(transform.position, desPos, lerpValue);   // Kamera pozisynu yumu�ak ge�i� ile aradaki mesafe kader uzaktan takip eder
        }
        else
        {
            Vector3 desPos = target.position + new Vector3(0,3,-14);
            transform.position= Vector3.Lerp(transform.position, desPos, lerpValue*0.5f*Time.deltaTime);
        }
        
    }
}
