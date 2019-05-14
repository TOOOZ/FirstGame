using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAiFrog : Enemies
{
    private PlayerController thePlayer;
    private Rigidbody2D rigidBody;
    public float speed;                 // скорость врага
    public float playerRange;           // радиус тревоги врага
    public LayerMask player;            // маска для определения Игрока
    public bool playerInRange;          // Условие при котором враг перестает следовать если игрок вышел из зоны тревоги
    public bool facingRight = false;    // поворот к игроку лицом
    public float jumpForce;             // сила прышка
    public bool grounded;               // проверка зе
    public bool waiting = false;        // задержка между прыжками (флаг)
    public Animator anim;
    public AudioSource jumpSound;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    IEnumerator Waiting()       //корутина отвечающзая за задержку между прыжками
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(3);
        waiting = false;
    }
	
	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag == "Ground")
        {   Debug.Log(col);
            anim.SetBool("Jump", false);
            grounded = true;
        }
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
        if (col.gameObject.tag == "Ground")
            grounded =false;
	}


    // Update is called once per frame
    void Update()
    {
       
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, player); // делаем невидимыйвидимый круг обнаружения
        if (!grounded && waiting)
        {
           transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, speed * Time.deltaTime); //передвижение во время полета
        }
        if (playerInRange && grounded && !waiting)
        {
            jumpSound.Play();
            rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);  //прыжок
            anim.SetBool("Jump", true); //включ анимацию прыжка
            StartCoroutine(Waiting());  //старт корутины
        }
		
        if ((transform.position.x - thePlayer.transform.position.x) < 0 && !facingRight)
        {
            //поворот
            Flip();
        }
        //и в другую сторону
        else if ((transform.position.x - thePlayer.transform.position.x) > 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
