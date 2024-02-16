using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public GameObject Ladder;
    public Transform Parent;
    
   
    //merdivenin çapraz colliderý yanlardan çarparsan ana objeyi deactive edip havuza ekler
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Ladder.transform.SetParent(Parent);
            Ladder.SetActive(false);

        }

    }
}
