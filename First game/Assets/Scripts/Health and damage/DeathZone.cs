using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Health someHealth;

	
    // Start is called before the first frame update
   void OnTriggerEnter2D(Collider2D col)
   {
	  
	   if(col.gameObject.tag == "Player")   //проверка тега объекта gameObject 
	   {
			someHealth.TakingDamage(10);
	   }
   }
}
