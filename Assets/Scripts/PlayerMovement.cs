using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;   // �leri hareket h�z�
    [SerializeField] float controlSpeed;    // Y�n h�z�

    // Touches settings
    [SerializeField] bool isTouching;   // Ekrana temas kontrol�
    float touchesPosX;  // Dokunma mesafesi
    Vector3 direction;  // Hareket y�n�

  
    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        if (PlayerManager.instance.playerState == PlayerManager.PlayerState.Move)
        {   // E�er playerState(Player durumu) Move(Hareket ediyor) ise s�rekli ileri do�ru hareket eder
            transform.position += Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        }
        if (isTouching)
        {   // E�er ekrana temas olmu� ise 
            touchesPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
            // Mosue hareket y�n� neresi ise o y�nde controlSpeed de�ri h�z�nda o y�ne hareketin pozisyon de�erini al�r
        }
        transform.position = new Vector3(touchesPosX, transform.position.y, transform.position.z);
        // touchesPosX gelen de�ere g�re  y�nde hareket der
    }
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;  // Mosue sol tu�una ba�lm��sa aktif yap
        }
        else
        {
            isTouching = false; // Mosue sol tu�una bas�lmad�ysa pasif yap
        }
    }
}
