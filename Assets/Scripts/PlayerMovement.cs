using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;   // Ýleri hareket hýzý
    [SerializeField] float controlSpeed;    // Yön hýzý
    [SerializeField] private float defaultSwipe = 3.4f;    // Player default kaydýrma mesafesi

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
        float moveX = transform.position.x; // Player objesinin x pozisyonun deðerini alýr
        transform.Translate(0, 0, moveSpeed * Time.fixedDeltaTime); // Player objesi oyun baþladýðýnda sürekli ileri hareket eder
        if (Input.GetKey(KeyCode.LeftArrow) || MobileInput.Instance.swipeLeft)
        {   // Eðer klavyede sol ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeLeft deðeri True ise  Sola gider               
            moveX = Mathf.Clamp(moveX - 1 * controlSpeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);
            // Player objesininn x (sol) pozisyonundaki gideceði min-max sýnýrý belirler

        }
        else if (Input.GetKey(KeyCode.RightArrow) || MobileInput.Instance.swipeRight)
        {   // Eðer klavyede sað ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeRight deðeri True ise Saða gider         
            moveX = Mathf.Clamp(moveX + 1 * controlSpeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);
            // Player objesinin  x (sað) pozisyonundaki gideceði min-max sýnýrý belirler
        }
        else
        {
            rb.velocity = Vector3.zero; //Eðer sað-sol hareket yapýlmadýysa ArrowPlayer nesnesi sabit kalsýn
        }
        transform.position = new Vector3(moveX, transform.position.y, transform.position.z);
        // Player objesinin pozisyonun moveX deðerine x yönünde sað-sola hareket eder y ve z sabit kalýr
    }
}
