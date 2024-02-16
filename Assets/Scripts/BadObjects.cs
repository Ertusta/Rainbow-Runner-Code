using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BadObjects : MonoBehaviour
{
    public float Speed = -10;

    public GameObject Coin;
    

    public Transform Parent;
    int _level;

    
    void Start()
    {
        Invoke("LocationControl", 5);

    }

    //objeler havuzdan çýkarken 
    private void OnEnable()
    {
        //tekrar seçilmesin diye dýþarý alýndý
        gameObject.transform.SetParent(null);

        //coin önceden toplandýðýnda ana objeyle beraber aktif olmuyor manuel açmak gerekiyor
        Coin.SetActive(true);

        //seviyeye göre scalelenen hýz için level bilgisi 
        _level = GameMachine.Instance.Level;

    }

    void Update()
    {
        //Obje hareket      
        transform.Translate(0, 0, Speed * Time.deltaTime*_level);
    }

    //karaktere ve arka duvara çarpýnca obje yok oluyor
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.SetParent(Parent);
            gameObject.SetActive(false);

        }
    }

    //objeler çok hýzlý olduklarýnda arkadaki duvarla çarpýþmadan geçiyolar konuma göre havuza ekliyorum
    void LocationControl()
    {
        if(gameObject.transform.position.z < -15)
        {
            gameObject.transform.SetParent(Parent);
            gameObject.SetActive(false);
        }
    }
    
    
    

}
