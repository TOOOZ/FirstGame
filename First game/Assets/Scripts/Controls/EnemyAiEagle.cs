using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiEagle : Enemies
{
    private PlayerController thePlayer;
    private Rigidbody2D rigidBody;
    public float speed;                 // скорость врага
    public float playerRange;           // радиус тревоги врага
    public LayerMask player;            // маска для определения Игрока
    public bool playerInRangeFar;       // Условие при котором враг перестает следовать если игрок вышел из зоны тревоги
    public bool facingRight = true;     // поворот к игроку лицом
	public float rapidAttack;			// круг для определения резкой атаки
	public bool playerInRangeClose;    // Игрок достаточно близок для атаки
	public bool attacking;
	private Vector3 playerPosition;
	


    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
		rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRangeFar = Physics2D.OverlapCircle(transform.position, playerRange, player); // делаем невидимыйвидимый круг обнаружения
		playerInRangeClose = Physics2D.OverlapCircle(transform.position,rapidAttack,player); // второй круг для атаки
        if (playerInRangeFar && !playerInRangeClose && !attacking)
		{
			transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, speed*Time.deltaTime);
		}
		if (playerInRangeClose  && !attacking)
		{
			StartCoroutine(TestAttack());
		}
			
        if ((transform.position.x - thePlayer.transform.position.x) < 0 && !facingRight && !attacking)
        {
            //поворот
            Flip();
        }
        //и в другую сторону
        else if ((transform.position.x - thePlayer.transform.position.x) > 0 && facingRight && !attacking)
        {
            // ... flip the player.
            Flip();
        }
		
		if ((transform.position.x - playerPosition.x) < 0 && !facingRight && attacking)
        {
            //поворот
            Flip();
        }
		
        //и в другую сторону
        else if ((transform.position.x - playerPosition.x) > 0 && facingRight && attacking)
        {
            // ... flip the player.
            Flip();
        }
	
    }
	
	IEnumerator TestAttack()
	{
			attacking = true;
			playerPosition = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y,
			thePlayer.transform.position.z);
			yield return new WaitForSecondsRealtime(1);
			StartCoroutine(SmoothMovement(playerPosition));

	}
	
	protected IEnumerator SmoothMovement (Vector3 end)
        {
            //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
            //Square magnitude is used instead of magnitude because it's computationally cheaper.
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            
            //While that distance is greater than a very small amount (Epsilon, almost zero):
			while(sqrRemainingDistance > float.Epsilon)
            {
                //Find a new position proportionally closer to the end, based on the moveTime
                Vector3 newPostion = Vector3.MoveTowards(transform.position, end, (speed*4*Time.deltaTime));
                Debug.Log(playerPosition);
                //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                rigidBody.MovePosition (newPostion);
                
                //Recalculate the remaining distance after moving.
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                
                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return null;
            }
			attacking = false;
		}
   
    void Flip()
    {
    facingRight = !facingRight;    
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
    }
}
