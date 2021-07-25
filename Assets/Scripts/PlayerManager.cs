using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Material collectedObjMat;    // Player ba�l� oldu�u materyal
    public PlayerState playerState; // Player hareket durumu (Stop-Move) 
    public LevelState levelState; // Level durumu   (NotFinished-Finished)

    public List<GameObject> collidedList;   // Toplanan objeleri bu listede tutar ve  eri�ir
    public Transform collectedPoolTransform;    // Toplanan objelerin tutuldu�u ana obje
    public Transform particlePrefab;    // �arp��ma efekti
    public enum PlayerState
    {
        Stop,   // Player hareket etmiyor
        Move    // Player hareket ediyor
    }
    public enum LevelState
    {
        NotFinished,    // Finish alan�na girmedi
        Finished    // Finish alan�na girdi
    }

    public static PlayerManager instance;   // Di�er scriptlerden eri�imi sa�lar
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
        {   // Toplanan objeler kadar fonkisoynu d�nd�r
            obj.GetComponent<CollectedObjController>().MakeSphere();
            // CollectedObjController scritinin MakeSphere methodunu �al��t�r
        }
    }
    public void RestartGame()
    {   // Fonksiyon her �al��t���nda o andaki sanhe yeniden ba�lat�l�r
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
