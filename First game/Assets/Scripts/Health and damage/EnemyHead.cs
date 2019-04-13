using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Controller player;
    public Enemies enemy;
    
 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Controller>().KillEnemy();
            if (col.gameObject.GetComponent<Controller>().grounded == false)
            {
                enemy.Dead();

            }
        }
        
    }
	
}
