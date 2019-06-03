using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public GameObject player;
    public Vector3 respawn;
    public int playerScore;
    public bool takeingDamage = false;
    public SpriteRenderer _sprite ;
	private bool flashing;
	private DateTime time;
	private bool spriteColorWhite=true;
	private Color flashColor = new Color(1f, 1f, 1f, 0f);
	private Color normalColor = new Color(1f, 1f, 1f, 1f);
	private Rigidbody2D rigidBody;
	public Image hP;
	public Text score;
    public SceneController gameOverMenu;
    public AudioSource stunSound;

    //[HideInInspector]
	public bool stun = false;
	
	

    //vector3 position= new vector3(-17.563,-1,0);

    // Start is called before the first frame update
    void Start()
    {
		score.text=" x "+playerScore;
		rigidBody = GetComponent<Rigidbody2D>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        respawn = new Vector3(-17, -2, 1);
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
        playerScore += sc;
		score.text=" x "+playerScore;
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
        yield return new WaitForSecondsRealtime(0.3f);
		stun=false;
		
    }

    public void Respawn()
    {
        player.transform.position = respawn;
        health = 5;
        hP.fillAmount = health;
        takeingDamage = false;
        flashing = false;
        _sprite.color = normalColor;
    }

    public void HpHeal()
    {
        if (health < 5 && health > 0)
        {
            health++;
            hP.fillAmount = health * 0.20f;
        }
    }

    // Update is called once per frame
    public void TakingDamage(float damage)
    {
        if (takeingDamage == false)
        {
            stunSound.Play();
            health -= damage;
			hP.fillAmount= health*0.20f;
            if (health <= 0)
            {
                gameOverMenu.GameOver();
                _sprite.color = flashColor;
            }
            else
            {
                stun = true;
                rigidBody.velocity = Vector2.zero;
                StartCoroutine(Stun());
                player.GetComponent<Animator>().SetBool("Damage", true);
                flashing = true;
                takeingDamage = true;
                time = DateTime.Now;
                StartCoroutine(Iframe());
            }
        }
		               
    }
}