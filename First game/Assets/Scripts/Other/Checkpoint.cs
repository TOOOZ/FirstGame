using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public Health player;
	public GameObject check2;
	
		
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>();
    }
	
	void OnTriggerEnter2D()
	{
		gameObject.SetActive(false);
		player.respawn = transform.position;
		check2.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
