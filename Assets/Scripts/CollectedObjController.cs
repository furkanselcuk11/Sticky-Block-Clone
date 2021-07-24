using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedObjController : MonoBehaviour
{
    [SerializeField] Transform sphere; 
    void Start()
    {
        sphere = transform.GetChild(0); // "CollectedObj" objesinin ilk çocuðu

        if (GetComponent <Rigidbody>()== null)
        {   // Eðer "CollectedObjController" scriptinin balý olduðu objede Rigidbody compenenti yoksa - Yeni bir obje toplanmýþsa
            gameObject.AddComponent<Rigidbody>();   // Yeni toplanan objeye Rigidbody componenti ekle
            Rigidbody rb = GetComponent<Rigidbody>();   // Rigidbody rb olarak tanýmla
            rb.useGravity = false;  // Gravity pasif yap 
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Renderer>().material = PlayerManager.instance.collectedObjMat; // Materyal componentine PlayerManager" scriptinden collectedObjMat ekle
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("collectibleObj"))
        {   // Eðer temas edilen objenin tagý "collectibleObj" ise 
            if (!PlayerManager.instance.collidedList.Contains(collision.gameObject))
            {   //  // Eðer collidedList listesindeki (Toplanan objeler) içinde temas edilen objeler yok ise
                collision.gameObject.tag = "collectedObj";  // Temas edilen objenin tagýný "collectedObj" yapar
                collision.transform.parent = PlayerManager.instance.collectedPoolTransform; // Temas edilen objeyi "collectedPoolTransform" objesinin alt objesi yapar
                PlayerManager.instance.collidedList.Add(collision.gameObject);  // Temas edilen objeyi "collidedList" ekler
                collision.gameObject.AddComponent<CollectedObjController>();    // Temas edilen objeye "CollectedObjController" scriptini ekler
            }            
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            DestroyTheObject(); // Temas edilen obje obstacle ise scritin baðlý olduðu objeyi yok eder
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectibleList"))
        {   // Eðer temas edilen objenin tagý "collectibleList" ise 
            other.gameObject.GetComponent<BoxCollider>().enabled = false;   // Temas edilen objenin BoxCollider componentinin enabled özelliði pasif olur
            other.transform.parent = PlayerManager.instance.collectedPoolTransform; // Temas edilen objeyi "collectedPoolTransform" objesinin alt objesi yapar
            foreach (Transform child in other.transform)
            {   // Temas edilen objenin çocuklarý kadar fonkisyonu döndür
                if (!PlayerManager.instance.collidedList.Contains(child.gameObject))
                {   // Eðer collidedList listesindeki (Toplanan objeler) içinde temas edilen objeler yok ise
                    PlayerManager.instance.collidedList.Add(child.gameObject);  // Temas edilen objeyi "collidedList" ekler
                    child.gameObject.tag = "collectedObj";  // Temas edilen objenin tagýný "collectedObj" yapar
                    child.gameObject.AddComponent<CollectedObjController>();    // Temas edilen objeye "CollectedObjController" scriptini ekler
                }
            }
        }
        if (other.gameObject.CompareTag("finishLine"))
        {   // Eðer temas edilen objenin tagý "finishLine" ise                        
            PlayerManager.instance.levelState = PlayerManager.LevelState.Finished;  // levelState (level durumu) Finished yap
            PlayerManager.instance.CallMakeSphere();    // Finish alanýna girince Cube olan objeleri sphere objesine çevir
        }
    }
    void DestroyTheObject()
    {   // Temas edilen objerli yok et ve toplanan objeler listesinden çýkar
        PlayerManager.instance.collidedList.Remove(gameObject); // Temas edilen objeyi listeden siler
        Destroy(gameObject);    // Temas edilen objeyi yok eder
        //  Temas edilen objenin temas ettiði pozisyonda Particle efekti oluþtur
        Transform particle = Instantiate(PlayerManager.instance.particlePrefab,transform.position,Quaternion.identity);
        particle.GetComponent<ParticleSystem>().startColor=PlayerManager.instance.collectedObjMat.color;
    }
    public void MakeSphere()
    {   // Finish alanýna girince Cube olan objeleri sphere objesine çevir
        gameObject.GetComponent<BoxCollider>().enabled = false;     // Scriptin baðlý olduðu objenin BoxCollider componentinin enabled özelliði pasif olur
        gameObject.GetComponent<MeshRenderer>().enabled = false;    // Scriptin baðlý olduðu objenin MeshRenderer componentinin enabled özelliði pasif olur

        sphere.gameObject.GetComponent<MeshRenderer>().enabled = true;  // sphere objesinin MeshRenderer componentinin enabled özelliði aktif olur
        sphere.gameObject.GetComponent<SphereCollider>().enabled = true;    // sphere objesinin SphereCollider componentinin enabled özelliði aktif olur
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = true;  // sphere objesinin SphereCollider componentinin isTrigger özelliði aktif olur
        sphere.gameObject.GetComponent<Renderer>().material = PlayerManager.instance.collectedObjMat;
        // sphere objesinin Materyal componentine PlayerManager" scriptinden collectedObjMat ekle
    }
    public void DropObj()
    {   // Hole (Delik) temas eden sphere objeleri
        sphere.gameObject.layer = 6;    // sphere objesine layer masek listesinden 6.sýradaki layeri ekler
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = false;   // sphere objesinin SphereCollider componentinin isTrigger özelliði pasif olur       
        sphere.gameObject.AddComponent<Rigidbody>();     // sphere objesine Rigidbody componenetini ekler
        sphere.GetComponent<Rigidbody>().useGravity = true; 
        // sphere objesinin SphereCollider componentinin useGravity özelliði aktif olur - Yerçekimi aktif olur ve düþer
    }
}
