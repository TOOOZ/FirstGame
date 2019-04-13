using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
	public GameObject deathPrefab;
	
    public void Dead()
    {
		GameObject deathAnim  = Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
