using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sphereObj"))
        {   // Temas eden objenin tag� "sphereObj" ise objenin ebeveyni olan CollectibleObj objesinin CollectedObjController scriptinin DropObj methodu �al���r
            other.transform.parent.GetComponent<CollectedObjController>().DropObj();
        }
    }
}
