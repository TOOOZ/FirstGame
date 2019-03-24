using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public GameObject player;
    public Vector3 respawn = new Vector3(-17,-2,1);
    public int score;
    public bool takeingDamage = false;
    public SpriteRenderer _sprite;
	private bool flashing;
	private DateTime time;
	private bool spriteColorWhite=true;
	private Color flashColor = new Color(1f, 1f, 1f, 0f);
	private Color normalColor = new Color(1f, 1f, 1f, 1f);

    //vector3 position= new vector3(-17.563,-1,0);

    // Start is called before the first frame update
    void Start()
    {
		
        gameObject.GetComponent<SpriteRenderer>();
    }

	void Update()
	{
		if(flashing)
		{	
			if((DateTime.Now-time).Milliseconds>=100)
			{
				time=DateTime.Now;
				if(spriteColorWhite)
				{
					_sprite.color=flashColor;
				} 
				else
				{
					_sprite.color=normalColor;
				}
				spriteColorWhite=!spriteColorWhite;
			}
			
		}
			
	}
	
    public void PickStar(int sc)
    {
        score += sc;
    }
	
	IEnumerator Iframe()       //корутина отвечающзая за задержку между прыжками
    {
		yield return new WaitForSecondsRealtime(2);
		takeingDamage=false;
		flashing=false;
	    _sprite.color=normalColor;
    }

    

    // Update is called once per frame
    public void TakingDamage(float damage)
    {
        if (takeingDamage == false)
        {
			
			flashing=true;
			takeingDamage=true;
			time=DateTime.Now;
			StartCoroutine(Iframe());			
            health -= damage;
            if (health <= 0)
            {
                player.SetActive(false);
                player.transform.position = respawn;
                player.SetActive(true);
                health = 10;
				takeingDamage=false;
				flashing=false;
            }
        }
		               
    }
}