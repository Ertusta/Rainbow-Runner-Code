using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMachine : MonoBehaviour
{
    public GameObject SidePool;
    public GameObject FrontPool;
    public GameObject Spawner;

    public GameObject MainUI;
    public GameObject GameUI;
  
    
    Transform _frontObject;
    Transform _sideObject;

    public int Level=1;


    public static GameMachine Instance;

    public AudioSource Theme;



    void Start()
    {
        
        //ekstra sahne eklemek yerine menude oyunu durdurur
        Time.timeScale = 0;

        //öndeki objeleri spawnlayan 
        InvokeRepeating("SpawnFront", 1, 5);

        //yandaki objeleri spawnlayan 
        InvokeRepeating("SpawnSide", 1, 5);

        InvokeRepeating("LevelCounter", 30,30 );

        Instance = this;


    }
 
    
    //yol üzerinde spawn olcaklar
     void SpawnFront()
    {
        //3 yoldan birini saç
        int randomNumber1 = Random.Range(2, 5);

        //rastgele objeyi seç
        _frontObject = FrontPool.transform.GetChild(Random.Range(0, FrontPool.transform.childCount ));

        //objeyi spawnla aktif et
        _frontObject.position = Spawner.transform.GetChild(randomNumber1).position;
        _frontObject.gameObject.SetActive(true);

        //kalan 2 yoldan birini seç
        int randomNumber2= Random.Range(2, 5);
        while(randomNumber1 == randomNumber2)
        {
             randomNumber2 = Random.Range(3, 5);
        }
        //ayný iþlem
        _frontObject = FrontPool.transform.GetChild(Random.Range(0, FrontPool.transform.childCount ));
        _frontObject.position = Spawner.transform.GetChild(randomNumber2).position;
        _frontObject.gameObject.SetActive(true);




    }
    //yanlarda spawn olcaklar
    void SpawnSide()
    {   //ayný iþlemler çift tarafa
        _sideObject = SidePool.transform.GetChild(Random.Range(0, SidePool.transform.childCount));
        _sideObject.position = Spawner.transform.GetChild(0).position;
        _sideObject.gameObject.SetActive(true);

        _sideObject = SidePool.transform.GetChild(Random.Range(0, SidePool.transform.childCount));
        _sideObject.position = Spawner.transform.GetChild(1).position;
        _sideObject.gameObject.SetActive(true);

    }

    //30 saniyede bir seviye atlatýr obje spawnlama sürelerini deðiþtirir
    void LevelCounter()
    {
        Level += 1;
        CancelInvoke("SpawnSide");
        CancelInvoke("SpawnFront");

        InvokeRepeating("SpawnFront", 1, 5f/Level);       
        InvokeRepeating("SpawnSide", 1, 5f/Level);
        
    }

    //oyun baþlayýnca
   public void StartButton()
    {

        MainUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1.0f;
        Theme.Play();
    }



}
