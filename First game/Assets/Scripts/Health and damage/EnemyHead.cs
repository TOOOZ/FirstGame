using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Controller player;
    public EnemyAiFrog frog;
    

    IEnumerator Animation()       //корутина отвечающзая за задержку между прыжками
    {
        frog.anim.SetBool("Dead", true);
        
        yield return new WaitForSecondsRealtime(1);
        frog.Dead();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.KillEnemy();
            if (player.grounded == false)
            {
                StartCoroutine(Animation());

            }
        }
        
    }
}
