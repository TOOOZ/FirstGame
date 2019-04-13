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
    public SpriteRenderer _sprite ;
	private bool flashing;
	private DateTime time;
	private bool spriteColorWhite=true;
	private Color flashColor = new Color(1f, 1f, 1f, 0f);
	private Color normalColor = new Color(1f, 1f, 1f, 1f);
	private Rigidbody2D rigidBody;
	
	[HideInInspector]
	public bool stun = false;
	
	

    //vector3 position= new vector3(-17.563,-1,0);

    // Start is called before the first frame update
    void Start()
    {
		rigidBody = GetComponent<Rigidbody2D>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
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
		yield return new WaitForSecondsRealtime(0.5f);
		player.GetComponent<Animator>().SetBool("Damage", false);
		yield return new WaitForSecondsRealtime(1.5f);
		takeingDamage=false;
		flashing=false;
	    _sprite.color=normalColor;
    }
	
	IEnumerator Stun()       //корутина отвечающзая за задержку между прыжками
    {
		
		yield return new WaitForSecondsRealtime(0.5f);
		stun=false;
		
    }

    

    // Update is called once per frame
    public void TakingDamage(float damage)
    {
        if (takeingDamage == false)
        {
			stun = true;
			rigidBody.velocity=Vector2.zero;
			StartCoroutine(Stun());
			player.GetComponent<Animator>().SetBool("Damage", true);
			Debug.Log("Получен урон");
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