using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{    
    //public Transform target;
    //[SerializeField] float smoothSpeed;
    //[SerializeField] Vector3 offset;

    [SerializeField] Transform target;    // Takip edilecek nesne
    [SerializeField] Vector3 offset;  // Ne kadar uzaklýktan  takip edecek

    [SerializeField] float lerpValue;
    void LateUpdate()
    {
        //if (PlayerManager.instance.levelState == PlayerManager.LevelState.NotFinished)
        //{
        //    Vector3 desiredPos = target.position + offset;
        //    Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        //    transform.position = new Vector3(transform.position.x, transform.position.y, smoothedPos.z);
        //}
        if(PlayerManager.instance.levelState == PlayerManager.LevelState.NotFinished)
        {
            Vector3 desPos = target.position + offset;  // Kamera ile takip edilen obje arasýndaki mesafe
            transform.position = Vector3.Lerp(transform.position, desPos, lerpValue);   // Kamera pozisynu yumuþak geçiþ ile aradaki mesafe kader uzaktan takip eder
        }
        else
        {
            Vector3 desPos = target.position + new Vector3(0,3,-14);
            transform.position= Vector3.Lerp(transform.position, desPos, lerpValue*0.5f*Time.deltaTime);
        }
        
    }
}
