using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFire : MonoBehaviour
{
    public Health someHealth;

    // Start is called before the first frame update
   void OnTriggerStay2D(Collider2D col)
   {
	   if(col.gameObject.tag == "Player")   //проверка тега объекта gameObject 
			someHealth.TakingDamage(1);
   }
    // Update is called once per frame
    void Update()
    {
             
    }
}
