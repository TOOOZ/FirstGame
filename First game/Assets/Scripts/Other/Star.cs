using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
	private bool pick=false;
    public AudioSource pickSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !pick)         //если игров попал в триггер
        {
            pickSound.Play();
			pick=true; 										 //Игрок заденет 2 коллайдерами и все сломаеться
			Destroy(gameObject);
			Debug.Log("+1");
            col.gameObject.GetComponent<Health>().PickStar(1);            
        }
    }
}
