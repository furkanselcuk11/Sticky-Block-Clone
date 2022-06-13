using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;   // Ýleri hareket hýzý
    [SerializeField] private float _horizontalspeed;    // Yön hýzý
    [SerializeField] private float defaultSwipe = 1.6f;    // Player default kaydýrma mesafesi

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (PlayerManager.instance.collidedList.Count == 0)
        {
            PlayerManager.instance.RestartGame();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(0, 0, _moveSpeed * Time.fixedDeltaTime);
        MoveInput();
    }

    void MoveInput()
    {
        float moveX = transform.position.x; // Player objesinin x pozisyonun deðerini alýr      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun deðerini alýr 

        if (Input.GetKey(KeyCode.LeftArrow) || MobileInput.instance.swipeLeft)
        {   // Eðer klavyede sol ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeLeft deðeri True ise  Sola hareket gider
            moveX = Mathf.Clamp(moveX - 1 * _horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa
            // Player objesinin x (sol) pozisyonundaki gideceði min-max sýnýrý belirler
            //moveX = moveX - 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon sýnýrlandýrýlmasý yoksa 
        }
        else if (Input.GetKey(KeyCode.RightArrow) || MobileInput.instance.swipeRight)
        {   // Eðer klavyede sað ok tuþuna basýldýysa yada "MobileInput" scriptinin swipeRight deðeri True ise Saða hareket gider  
            moveX = Mathf.Clamp(moveX + 1 * _horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon sýnýrlandýrýlmasý koyulacaksa
            // Player objesinin x (sað) pozisyonundaki gideceði min-max sýnýrý belirler
            //moveX = moveX + 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon sýnýrlandýrýlmasý yoksa 
        }
        else
        {
            rb.velocity = Vector3.zero; // Eðer hareket edilmediyse Player objesi sabit kalsýn
        }

        transform.position = new Vector3(moveX, transform.position.y, moveZ);
        // Player objesinin pozisyonu moveX deðerine göre x ekseninde, moveZ deðerine göre z ekseninde hareket eder ve y ekseninde sabit kalýr 
    }
}
