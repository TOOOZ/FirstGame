using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class PlayerController : MonoBehaviour
{
    private Controller playerController; // использование скрипта PlayerController
    public Animator anim;                     // подключение анимации      
    public bool crouch;
	public GameObject player;
	

    void Start()
    {
        playerController = GetComponent<Controller>();
        anim = GetComponent<Animator>();
		
    }

    public void Onlanding()
    {
        anim.SetBool("Jump", false);
		
    }

    public void OnCrouching(bool crouching)
    {
        anim.SetBool("Crouching", crouching);
		Debug.Log(crouching);
   }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerController.Jump();
            anim.SetBool("Jump", true);
        }
		
        
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        playerController.Move(Input.GetAxis("Horizontal"), crouch);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); // Используется анимация бега при значения скорости больше 0 (берется модуль так как движение влево равно -1)
    }

}
