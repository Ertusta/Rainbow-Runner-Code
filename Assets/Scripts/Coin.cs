using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
   

    //karaktere �arp�nca kaybolan obje
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {           
            gameObject.SetActive(false);
        }

    }
}
