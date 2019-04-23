using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float runSpeed = 8f;                 //скорость бега
    public float crouchSpeed = .36f;            //скорость ползанья
    public float jumpForce = 5f;                //сила прыжка
    public float ceilingRadius = .07f;           // радиус круга при котором персонаж может встать
    public float movementSmoothing = .08f;      //Плавность движения
    public bool grounded;                       //флаг земли
    public bool facingRight = true;             // В какцую сторону персонаж смотрит
    private Vector3 velocity = Vector3.zero;     //скорость
	private Vector2 jump = Vector2.right;
    private Rigidbody2D rigidBody;
    public PlayerController player;	            // скрипт персонажа
    public Transform сeilingCheck;              //проверка  на препятствия над головой
    public Transform groundCheck;                //Проверка земли
    public Collider2D crouchDisableCollider;    //колайдер выключается 
    public LayerMask whatIsGround;              //определение что есть земля
	
	
   
    [Header("Events")]                          // Заголовок события    
    [Space]                                     // Пустое место

    public UnityEvent OnLandEvent;              //событие определяющее что персонаж стоит на земле

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { } 

    public BoolEvent OnCrouchEvent;
    public bool wasCrouching = false;           // Ползет ли персонаж


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = gameObject.GetComponentInParent<PlayerController>();

    }

    private void Awake()
    {
        if (OnLandEvent == null)                // конструктор эвентов, создает список который потом используеться
            OnLandEvent = new UnityEvent();
        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;            //Переменная для определения был ли персонаж на земле
        grounded = false;                       

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, ceilingRadius, whatIsGround);  // проверка с помощью коллайдера на заземление
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;                //если коллайдер косается спрайта с тегом Ground то он на земле
                if (!wasGrounded)               // если он был в полете то проигрывается анимация прыжка
                    OnLandEvent.Invoke();
                
            }
        }
    }

  
    public void Jump()
    {
        if (grounded) //проверка на заземление
        {
            rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);   // расчет силы прыжка
        } 
        
        
    }

    public void KillEnemy()
    {
        if (!grounded)
		{
		 rigidBody.velocity=Vector2.right;
         rigidBody.AddForce(transform.up *jumpForce, ForceMode2D.Impulse);
		}
    }
   
    public void Move(float ax,bool crouch)
    {
        
        if (!crouch)
        {
            // Если у персонажа есть потолок, мешающий ему встать, держите его приседающим
            if (Physics2D.OverlapCircle(сeilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }
        if (crouch)
        {
            if (!wasCrouching)
            {
                Debug.Log(wasCrouching = true);
                ((MonoBehaviour)OnCrouchEvent.GetPersistentTarget(0))
					.SendMessage(OnCrouchEvent.GetPersistentMethodName(0), wasCrouching); 
            }
            // Скорость ползанья
            ax *= crouchSpeed;

            // Выключения коллайдера когда ползет
            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = false;
        }
        else
        {
            // Включение коллайдера при вставании
            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = true;

            if (wasCrouching)
            {
                Debug.Log(wasCrouching = false);
                ((MonoBehaviour)OnCrouchEvent.GetPersistentTarget(0))
					.SendMessage(OnCrouchEvent.GetPersistentMethodName(0), wasCrouching);
            }
        }

        Vector3 targetVelocity = new Vector2(ax * runSpeed, rigidBody.velocity.y); //движение
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);

        //если игрок движеться вправо а персонаж смотрит влево
        if (ax > 0 && !facingRight)
        {
            //поворот
            Flip();
        }
        //и в другую сторону
        else if (ax < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
     }

    public void Flip()
    {
        // персонаж смотрит на право
        facingRight = !facingRight;

        // Умножение поворота спрайта игрока на -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}    