using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
   void OnTriggerStay2D(Collider2D col)
   {
	   if(col.gameObject.tag == "Player")   //проверка тега объекта gameObject 
	   {
			col.gameObject.GetComponent<Health>().TakingDamage(damage);			
	   }
   }

}

