using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Material collectedObjMat;    // Player baðlý olduðu materyal
    public PlayerState playerState; // Player hareket durumu (Stop-Move) 
    public LevelState levelState; // Level durumu   (NotFinished-Finished)

    public List<GameObject> collidedList;   // Toplanan objeleri bu listede tutar ve  eriþir
    public Transform collectedPoolTransform;    // Toplanan objelerin tutulduðu ana obje
    public Transform particlePrefab;    // Çarpýþma efekti
    public enum PlayerState
    {
        Stop,   // Player hareket etmiyor
        Move    // Player hareket ediyor
    }
    public enum LevelState
    {
        NotFinished,    // Finish alanýna girmedi
        Finished    // Finish alanýna girdi
    }

    public static PlayerManager instance;   // Diðer scriptlerden eriþimi saðlar
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
    public void CallMakeSphere()
    {
        foreach (GameObject obj in collidedList)
        {   // Toplanan objeler kadar fonkisoynu döndür
            obj.GetComponent<CollectedObjController>().MakeSphere();
            // CollectedObjController scritinin MakeSphere methodunu çalýþtýr
        }
    }
    public void RestartGame()
    {   // Fonksiyon her çalýþtýðýnda o andaki sanhe yeniden baþlatýlýr
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
