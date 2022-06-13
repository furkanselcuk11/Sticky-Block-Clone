using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    private Rigidbody rb;
    public bool isGrounded;    // Zemine Temas ettimi Kontrol
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material = PlayerManager.instance.collectedObjMat; 
        // Oyun ba�lad���nda CollectedObj(Player) objesinin materyali PlayerManager'de tan�ml� collectedObjMat e�itlenir
        PlayerManager.instance.collidedList.Add(gameObject);    // Oyun ba�lad���nda scriptib ba�l� oldu�u obje listeye eklenir
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {   // Temas ediilen objenin tag� "ground" ise  - Zemine temas etmi�se
            Grounded(); // Methodu �al��t�r�l�r
        }
    }
    void Grounded()
    {
        isGrounded = true;  // Zemine temas edildi - Aktif
        PlayerManager.instance.playerState = PlayerManager.PlayerState.Move;    // Player durumu move(hareket) ediyor olur 
        rb.useGravity = false;  // Gravt de�eri false olur
        rb.constraints = RigidbodyConstraints.FreezeAll;

        Destroy(this, 1); // Zemin ile temas ettikten 1 saniye sonra script silinir
    }
}
