using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHeal : MonoBehaviour
{
    public Health player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.HpHeal();
        }
    }
}
