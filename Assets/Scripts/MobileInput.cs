using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput instance;   // Diðer Script'ler üzrerinden eriþimi saðlar

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
        // Bütün boollarý sýfýrlýyoruz
        tap = swipe = false;
        swipeLeft = false;  // Sola kaydýrma
        swipeRight = false; // Saða kaydýrma
    }
    private void FixedUpdate()
    {
        SwipeMove();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   // Mosue tuþuna baýldýðýnda veya ekranda parmak ile basýldýðýndaki ilk pozisyon deðerini alýr
            start_pos = Input.mousePosition;    // Ýlk pozisyon deðeri tutulur
            tap = true; // Dokunma aktif olur
        }

        if (Input.GetMouseButton(0))
        {   // Mosue tuþuna baýlý tutulduðunda veya ekranda parmak ile basýlý tutularak gidildiðindeki son pozisyonun deðerini alýr
            last_pos = Input.mousePosition; // Son pozisyon deðeri tutulur
            delta = start_pos - last_pos;   // Toplam kaydýrýlan mesafe hesaplanýr ve delta deðerinde tutulur
            swipe = true;   // Kaydýrma aktif olur

        }

        if (Input.GetMouseButtonUp(0))
        {   // Mosue tuþuna basma býrakýldýðýnda veya ekranda parmak basma býrakýldýðýnda 
            if (start_pos == last_pos) swipe = false;
            // Eðer dokunulan ilk pozisyon ile son pozisyon deðeri ayný ise kaydýrma pasif olur
            start_pos = Vector2.zero;
            last_pos = Vector2.zero;
            delta = Vector2.zero;
            // Tüm deðerler sýfýrlanýr tekrar dokunma iþlemine kadar

            swipeRight = false;
            swipeLeft = false;
            tap = false;
            // Tüm bool deðerler sýfýrlanýr tekrar dokunma iþlemine kadar
        }
    }
    void SwipeMove()
    {   // Kaydýrma hareketinin yönünü belirler
        // "PlayerMovement" scripti üzerinde "MoveInput" fonksiyonu saða veya sola hareket yönünü belirtir
        if (tap)    // Eðer dokunma iþlemi aktif ise çalýþýr
        {
            if (swipe)  // Eðer swipe(kaydýrma) iþlemi aktif ise çalýþýr
            {
                if (delta.magnitude > 100)  // delta deðerinin uzunluk bilgisini alýr ve 100 deðerinden büyükse çalýþýr - 100 deðeri minimum kaydýrma mesafesi
                {
                    float x = delta.x;  // Kayrýma mesafesinin x deðerini alýr
                    float y = delta.y;  // Kayrýma mesafesinin y deðerini alýr
                    if (Mathf.Abs(x) > Mathf.Abs(y))    // Eðer kaydýrma mesafesinin x ekseni y ekseninden daha büyükse (Right-Left) deilse (Up-Down) kaydýrma aktif olur
                    {
                        // Right-Left
                        if (x < 0)
                        {
                            // Saða kaydýrma aktif olur 
                            swipeRight = true;
                            swipeLeft = false;
                            swipeUp = false;
                            swipeDown = false;
                        }
                        else
                        {
                            // Sola kaydýrma aktif olur 
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
                            // Ýleri kaydýrma aktif olur 
                            swipeRight = false;
                            swipeLeft = false;
                            swipeUp = true;
                            swipeDown = false;
                        }
                        else
                        {
                            // Geri kaydýrma aktif olur 
                            swipeRight = false;
                            swipeLeft = false;
                            swipeUp = false;
                            swipeDown = true;
                        }
                    }
                }
            }
            else
            {   // Eðer kaydýrma iþlemi pasif ise 
                tap = false;    // Dokunma pasif olur
            }
        }
    }
}
