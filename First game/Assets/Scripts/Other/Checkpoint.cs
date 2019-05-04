using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public Health player;
	public GameObject check2;
    private PlayerController thePlayer;
	
		
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>();
        thePlayer = FindObjectOfType<PlayerController>();
    }
	
	void OnTriggerEnter2D()
	{
        if (thePlayer)
        {
            gameObject.SetActive(false);
            player.respawn = transform.position;
            check2.SetActive(true);
        }
	}

}
