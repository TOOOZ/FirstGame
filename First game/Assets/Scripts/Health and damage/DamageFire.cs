using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFire : MonoBehaviour
{
    public Health someHealth;
    public int damage;
	public Collider2D head;

	IEnumerator Head()       //корутина отвечающзая за задержку между прыжками
    {
		head.enabled = false;
		yield return new WaitForSecondsRealtime(0.8f);
		head.enabled = true;
    }
	
    // Start is called before the first frame update
   void OnTriggerStay2D(Collider2D col)
   {
	   if(col.gameObject.tag == "Player")   //проверка тега объекта gameObject 
	   {
			col.gameObject.GetComponent<Health>().TakingDamage(damage);
			StartCoroutine(Head());			
	   }
   }

}
