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

    //objeler havuzdan ��karken 
    private void OnEnable()
    {
        //tekrar se�ilmesin diye d��ar� al�nd�
        gameObject.transform.SetParent(null);

        //coin �nceden topland���nda ana objeyle beraber aktif olmuyor manuel a�mak gerekiyor
        Coin.SetActive(true);

        //seviyeye g�re scalelenen h�z i�in level bilgisi 
        _level = GameMachine.Instance.Level;

    }

    void Update()
    {
        //Obje hareket      
        transform.Translate(0, 0, Speed * Time.deltaTime*_level);
    }

    //karaktere ve arka duvara �arp�nca obje yok oluyor
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.SetParent(Parent);
            gameObject.SetActive(false);

        }
    }

    //objeler �ok h�zl� olduklar�nda arkadaki duvarla �arp��madan ge�iyolar konuma g�re havuza ekliyorum
    void LocationControl()
    {
        if(gameObject.transform.position.z < -15)
        {
            gameObject.transform.SetParent(Parent);
            gameObject.SetActive(false);
        }
    }
    
    
    

}
