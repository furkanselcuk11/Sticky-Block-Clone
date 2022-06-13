using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedObjController : MonoBehaviour
{
    [SerializeField] Transform sphere; 
    void Start()
    {
        sphere = transform.GetChild(0); // "CollectedObj" objesinin ilk �ocu�u

        if (GetComponent <Rigidbody>()== null)
        {
            // E�er "CollectedObjController" scriptinin ba�l� oldu�u objede Rigidbody compenenti yoksa 
            gameObject.AddComponent<Rigidbody>();   // Yeni toplanan objeye Rigidbody componenti ekle
            Rigidbody rb = GetComponent<Rigidbody>();   // Rigidbody rb olarak tan�mla
            rb.useGravity = false;  // Gravity pasif yap 
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Renderer>().material = PlayerManager.instance.collectedObjMat; // Materyal componentine PlayerManager" scriptinden collectedObjMat ekler ve toplanan objenin rengini de�i�ir
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("collectibleObj"))
        {   // Yeni bir obje toplanm��sa
            // E�er temas edilen objenin tag� "collectibleObj" ise 
            if (!PlayerManager.instance.collidedList.Contains(collision.gameObject))
            {   // E�er collidedList listesindeki (Toplanan objeler) i�inde temas edilen objeler yok ise    / Contains=icerir
                collision.gameObject.tag = "collectedObj";  // Temas edilen objenin tag�n� "collectedObj" yapar - Topland� olarak atar
                collision.transform.parent = PlayerManager.instance.collectedPoolTransform; // Temas edilen objeyi "collectedPoolTransform" objesinin alt objesi yapar
                PlayerManager.instance.collidedList.Add(collision.gameObject);  // Temas edilen objeyi "collidedList" listesine ekler
                collision.gameObject.AddComponent<CollectedObjController>();    // Temas edilen objeye "CollectedObjController" scriptini ekler
            }            
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            DestroyTheObject(); // Temas edilen obje obstacle ise scritin ba�l� oldu�u objeyi yok eder
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectibleList"))
        {   // Yeni objeler toplanm��sa
            // E�er temas edilen objenin tag� "collectibleList" ise 
            other.gameObject.GetComponent<BoxCollider>().enabled = false;   // Temas edilen objenin BoxCollider componentinin enabled �zelli�i pasif olur
            other.transform.parent = PlayerManager.instance.collectedPoolTransform; // Temas edilen objeyi "collectedPoolTransform" objesinin alt objesi yapar
            foreach (Transform child in other.transform)
            {   // Temas edilen objenin �ocuklar� kadar fonkisyonu d�nd�r
                if (!PlayerManager.instance.collidedList.Contains(child.gameObject))
                {   // E�er collidedList listesindeki (Toplanan objeler) i�inde temas edilen objeler yok ise / Contains=icerir
                    PlayerManager.instance.collidedList.Add(child.gameObject);  // Temas edilen objeyi "collidedList" listesine ekler
                    child.gameObject.tag = "collectedObj";  // Temas edilen objenin tag�n� "collectedObj" yapar - Topland� olarak atar
                    child.gameObject.AddComponent<CollectedObjController>();    // Temas edilen objeye "CollectedObjController" scriptini ekler
                }
            }
        }
        if (other.gameObject.CompareTag("finishLine"))
        {   // E�er temas edilen objenin tag� "finishLine" ise                        
            PlayerManager.instance.levelState = PlayerManager.LevelState.Finished;  // levelState (level durumu) Finished yap
            PlayerManager.instance.CallMakeSphere();    // Finish alan�na girince Cube olan objeleri sphere objesine �evir
        }
    }
    void DestroyTheObject()
    {   // Temas edilen objeleri yok et ve toplanan objeler listesinden ��kar
        PlayerManager.instance.collidedList.Remove(gameObject); // Temas edilen objeyi listeden siler
        Destroy(gameObject);    // Temas edilen objeyi yok eder
        //  Temas edilen objenin temas etti�i pozisyonda Particle efekti olu�tur
        Transform particle = Instantiate(PlayerManager.instance.particlePrefab,transform.position,Quaternion.identity);
#pragma warning disable CS0618 // Type or member is obsolete
        particle.GetComponent<ParticleSystem>().startColor=PlayerManager.instance.collectedObjMat.color;
        // particle efektin ba�lang�� rengini (Player)objesinin collectedObjMat Materyali ile ayn� yapar
#pragma warning restore CS0618 // Type or member is obsolete
    }
    public void MakeSphere()
    {   // Finish alan�na girince Cube olan objeleri sphere objesine �evir
        gameObject.GetComponent<BoxCollider>().enabled = false;     // Scriptin ba�l� oldu�u objenin BoxCollider componentinin enabled �zelli�i pasif olur
        gameObject.GetComponent<MeshRenderer>().enabled = false;    // Scriptin ba�l� oldu�u objenin MeshRenderer componentinin enabled �zelli�i pasif olur

        sphere.gameObject.GetComponent<MeshRenderer>().enabled = true;  // sphere objesinin MeshRenderer componentinin enabled �zelli�i aktif olur
        sphere.gameObject.GetComponent<SphereCollider>().enabled = true;    // sphere objesinin SphereCollider componentinin enabled �zelli�i aktif olur
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = true;  // sphere objesinin SphereCollider componentinin isTrigger �zelli�i aktif olur
        sphere.gameObject.GetComponent<Renderer>().material = PlayerManager.instance.collectedObjMat;
        // sphere objesinin Materyal componentine PlayerManager" scriptinden collectedObjMat materyalinin rengine ata
    }
    public void DropObj()
    {   // Hole (Delik) temas eden sphere objeleri
        sphere.gameObject.layer = 6;    // sphere objesine layer mask listesinden 6.s�radaki layeri ekler
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = false;   // sphere objesinin SphereCollider componentinin isTrigger �zelli�i pasif olur       
        sphere.gameObject.AddComponent<Rigidbody>();     // sphere objesine Rigidbody componenetini ekler
        sphere.GetComponent<Rigidbody>().useGravity = true; 
        // sphere objesinin SphereCollider componentinin useGravity �zelli�i aktif olur - Yer�ekimi aktif olur ve d��er
    }
}
