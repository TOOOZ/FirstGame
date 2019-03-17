using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiFrog : MonoBehaviour
{
    private PlayerController thePlayer;
    private Rigidbody2D rigidBody;
    public float speed;                 // скорость врага
    public float playerRange;           // радиус тревоги врага
    public LayerMask player;            // маска для определения Игрока
    public bool playerInRange;       // Условие при котором враг перестает следовать если игрок вышел из зоны тревоги
    public bool facingRight = false;     // поворот к игроку лицом
    public float jumpForce;             // сила прышка
    public bool grounded;               // проверка земли
    public Transform groundCheck;       // проверка земли
    public LayerMask whatIsGround;



    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .07f, whatIsGround);  // проверка с помощью коллайдера на заземление
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
              Debug.Log(grounded = true);                //если коллайдер косается спрайта с тегом Ground то он на земле
            }
            else
            {
                Debug.Log(grounded = false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, player); // делаем невидимыйвидимый круг обнаружения
        if (playerInRange && !grounded)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, speed * Time.deltaTime);
        }
        if (playerInRange && grounded)
        {
            rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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
        Debug.Log(facingRight = !facingRight);
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
