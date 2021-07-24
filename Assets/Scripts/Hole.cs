using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sphereObj"))
        {   // Temas eden objenin tagý "sphereObj" ise objenin ebeveyni olan CollectibleObj objesinin CollectedObjController scriptinin DropObj methodu çalýþýr
            other.transform.parent.GetComponent<CollectedObjController>().DropObj();
        }
    }
}
