using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
   

    //karaktere çarpýnca kaybolan obje
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {           
            gameObject.SetActive(false);
        }

    }
}
