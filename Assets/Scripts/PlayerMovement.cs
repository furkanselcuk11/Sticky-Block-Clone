using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;   // �leri hareket h�z�
    [SerializeField] float controlSpeed;    // Y�n h�z�
    [SerializeField] private float defaultSwipe = 3.4f;    // Player default kayd�rma mesafesi

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
  
    private void FixedUpdate()
    {
        MoveInput();
    }

    void MoveInput()
    {
        float moveX = transform.position.x; // Player objesinin x pozisyonun de�erini al�r
        transform.Translate(0, 0, moveSpeed * Time.fixedDeltaTime); // Player objesi oyun ba�lad���nda s�rekli ileri hareket eder
        if (Input.GetKey(KeyCode.LeftArrow) || MobileInput.Instance.swipeLeft)
        {   // E�er klavyede sol ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeLeft de�eri True ise  Sola gider               
            moveX = Mathf.Clamp(moveX - 1 * controlSpeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);
            // Player objesininn x (sol) pozisyonundaki gidece�i min-max s�n�r� belirler

        }
        else if (Input.GetKey(KeyCode.RightArrow) || MobileInput.Instance.swipeRight)
        {   // E�er klavyede sa� ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeRight de�eri True ise Sa�a gider         
            moveX = Mathf.Clamp(moveX + 1 * controlSpeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);
            // Player objesinin  x (sa�) pozisyonundaki gidece�i min-max s�n�r� belirler
        }
        else
        {
            rb.velocity = Vector3.zero; //E�er sa�-sol hareket yap�lmad�ysa ArrowPlayer nesnesi sabit kals�n
        }
        transform.position = new Vector3(moveX, transform.position.y, transform.position.z);
        // Player objesinin pozisyonun moveX de�erine x y�n�nde sa�-sola hareket eder y ve z sabit kal�r
    }
}
