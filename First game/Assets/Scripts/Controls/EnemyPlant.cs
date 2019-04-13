using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : Enemies
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	public LayerMask player; 
	public bool	playerInRange;
	public Animator anim;
	 
	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	IEnumerator Attack()
	{	
			anim.SetBool("Attack",true);
			yield return new WaitForSecondsRealtime(0.65f);
			Instantiate (shot,shotSpawn.position,shotSpawn.rotation);
			anim.SetBool("Attack",false);
	}

    // Update is called once per frame
     void Update()
    {
		if(playerInRange = Physics2D.OverlapCircle(transform.position, 11, player))
		{
			anim.SetBool("Idle",false);
			if(Time.time > nextFire)
			{		
			nextFire = Time.time + fireRate;
			StartCoroutine(Attack());
			}
		}
		else
			anim.SetBool("Idle",true);
    }
}
