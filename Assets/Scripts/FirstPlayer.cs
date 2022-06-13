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
        // Oyun baþladýðýnda CollectedObj(Player) objesinin materyali PlayerManager'de tanýmlý collectedObjMat eþitlenir
        PlayerManager.instance.collidedList.Add(gameObject);    // Oyun baþladýðýnda scriptib baðlý olduðu obje listeye eklenir
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {   // Temas ediilen objenin tagý "ground" ise  - Zemine temas etmiþse
            Grounded(); // Methodu çalýþtýrýlýr
        }
    }
    void Grounded()
    {
        isGrounded = true;  // Zemine temas edildi - Aktif
        PlayerManager.instance.playerState = PlayerManager.PlayerState.Move;    // Player durumu move(hareket) ediyor olur 
        rb.useGravity = false;  // Gravt deðeri false olur
        rb.constraints = RigidbodyConstraints.FreezeAll;

        Destroy(this, 1); // Zemin ile temas ettikten 1 saniye sonra script silinir
    }
}
