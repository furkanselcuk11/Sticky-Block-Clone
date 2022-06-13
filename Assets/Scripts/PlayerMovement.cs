using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;   // �leri hareket h�z�
    [SerializeField] private float _horizontalspeed;    // Y�n h�z�
    [SerializeField] private float defaultSwipe = 1.6f;    // Player default kayd�rma mesafesi

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
        float moveX = transform.position.x; // Player objesinin x pozisyonun de�erini al�r      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun de�erini al�r 

        if (Input.GetKey(KeyCode.LeftArrow) || MobileInput.instance.swipeLeft)
        {   // E�er klavyede sol ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeLeft de�eri True ise  Sola hareket gider
            moveX = Mathf.Clamp(moveX - 1 * _horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon s�n�rland�r�lmas� koyulacaksa
            // Player objesinin x (sol) pozisyonundaki gidece�i min-max s�n�r� belirler
            //moveX = moveX - 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon s�n�rland�r�lmas� yoksa 
        }
        else if (Input.GetKey(KeyCode.RightArrow) || MobileInput.instance.swipeRight)
        {   // E�er klavyede sa� ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeRight de�eri True ise Sa�a hareket gider  
            moveX = Mathf.Clamp(moveX + 1 * _horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon s�n�rland�r�lmas� koyulacaksa
            // Player objesinin x (sa�) pozisyonundaki gidece�i min-max s�n�r� belirler
            //moveX = moveX + 1 * horizontalspeed * Time.fixedDeltaTime;    // Pozisyon s�n�rland�r�lmas� yoksa 
        }
        else
        {
            rb.velocity = Vector3.zero; // E�er hareket edilmediyse Player objesi sabit kals�n
        }

        transform.position = new Vector3(moveX, transform.position.y, moveZ);
        // Player objesinin pozisyonu moveX de�erine g�re x ekseninde, moveZ de�erine g�re z ekseninde hareket eder ve y ekseninde sabit kal�r 
    }
}
