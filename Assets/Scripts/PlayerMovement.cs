using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;   // Ýleri hareket hýzý
    [SerializeField] float controlSpeed;    // Yön hýzý

    // Touches settings
    [SerializeField] bool isTouching;   // Ekrana temas kontrolü
    float touchesPosX;  // Dokunma mesafesi
    Vector3 direction;  // Hareket yönü

  
    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        if (PlayerManager.instance.playerState == PlayerManager.PlayerState.Move)
        {   // Eðer playerState(Player durumu) Move(Hareket ediyor) ise sürekli ileri doðru hareket eder
            transform.position += Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        }
        if (isTouching)
        {   // Eðer ekrana temas olmuþ ise 
            touchesPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
            // Mosue hareket yönü neresi ise o yönde controlSpeed deðri hýzýnda o yöne hareketin pozisyon deðerini alýr
        }
        transform.position = new Vector3(touchesPosX, transform.position.y, transform.position.z);
        // touchesPosX gelen deðere göre  yönde hareket der
    }
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;  // Mosue sol tuþuna baýlmýþsa aktif yap
        }
        else
        {
            isTouching = false; // Mosue sol tuþuna basýlmadýysa pasif yap
        }
    }
}
