using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput instance;   // Di�er Script'ler �zrerinden eri�imi sa�lar

    // Mouse Positions
    private Vector2 start_pos;
    Vector2 last_pos;
    Vector2 delta;

    [Header("Controllers")]
    public bool tap;
    public bool swipeLeft;
    public bool swipeRight;
    public bool swipeUp;
    public bool swipeDown;
    public bool swipe;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // B�t�n boollar� s�f�rl�yoruz
        tap = swipe = false;
        swipeLeft = false;  // Sola kayd�rma
        swipeRight = false; // Sa�a kayd�rma
    }
    private void FixedUpdate()
    {
        SwipeMove();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   // Mosue tu�una ba�ld���nda veya ekranda parmak ile bas�ld���ndaki ilk pozisyon de�erini al�r
            start_pos = Input.mousePosition;    // �lk pozisyon de�eri tutulur
            tap = true; // Dokunma aktif olur
        }

        if (Input.GetMouseButton(0))
        {   // Mosue tu�una ba�l� tutuldu�unda veya ekranda parmak ile bas�l� tutularak gidildi�indeki son pozisyonun de�erini al�r
            last_pos = Input.mousePosition; // Son pozisyon de�eri tutulur
            delta = start_pos - last_pos;   // Toplam kayd�r�lan mesafe hesaplan�r ve delta de�erinde tutulur
            swipe = true;   // Kayd�rma aktif olur

        }

        if (Input.GetMouseButtonUp(0))
        {   // Mosue tu�una basma b�rak�ld���nda veya ekranda parmak basma b�rak�ld���nda 
            if (start_pos == last_pos) swipe = false;
            // E�er dokunulan ilk pozisyon ile son pozisyon de�eri ayn� ise kayd�rma pasif olur
            start_pos = Vector2.zero;
            last_pos = Vector2.zero;
            delta = Vector2.zero;
            // T�m de�erler s�f�rlan�r tekrar dokunma i�lemine kadar

            swipeRight = false;
            swipeLeft = false;
            tap = false;
            // T�m bool de�erler s�f�rlan�r tekrar dokunma i�lemine kadar
        }
    }
    void SwipeMove()
    {   // Kayd�rma hareketinin y�n�n� belirler
        // "PlayerMovement" scripti �zerinde "MoveInput" fonksiyonu sa�a veya sola hareket y�n�n� belirtir
        if (tap)    // E�er dokunma i�lemi aktif ise �al���r
        {
            if (swipe)  // E�er swipe(kayd�rma) i�lemi aktif ise �al���r
            {
                if (delta.magnitude > 100)  // delta de�erinin uzunluk bilgisini al�r ve 100 de�erinden b�y�kse �al���r - 100 de�eri minimum kayd�rma mesafesi
                {
                    float x = delta.x;  // Kayr�ma mesafesinin x de�erini al�r
                    float y = delta.y;  // Kayr�ma mesafesinin y de�erini al�r
                    if (Mathf.Abs(x) > Mathf.Abs(y))    // E�er kayd�rma mesafesinin x ekseni y ekseninden daha b�y�kse (Right-Left) deilse (Up-Down) kayd�rma aktif olur
                    {
                        // Right-Left
                        if (x < 0)
                        {
                            // Sa�a kayd�rma aktif olur 
                            swipeRight = true;
                            swipeLeft = false;
                            swipeUp = false;
                            swipeDown = false;
                        }
                        else
                        {
                            // Sola kayd�rma aktif olur 
                            swipeRight = false;
                            swipeLeft = true;
                            swipeUp = false;
                            swipeDown = false;
                        }
                    }
                    else
                    {
                        // Up-Down
                        if (y < 0)
                        {
                            // �leri kayd�rma aktif olur 
                            swipeRight = false;
                            swipeLeft = false;
                            swipeUp = true;
                            swipeDown = false;
                        }
                        else
                        {
                            // Geri kayd�rma aktif olur 
                            swipeRight = false;
                            swipeLeft = false;
                            swipeUp = false;
                            swipeDown = true;
                        }
                    }
                }
            }
            else
            {   // E�er kayd�rma i�lemi pasif ise 
                tap = false;    // Dokunma pasif olur
            }
        }
    }
}
