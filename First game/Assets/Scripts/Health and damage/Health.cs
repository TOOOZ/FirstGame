using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public GameObject player;
    public Vector3 respawn = new Vector3(-17,-2,1);
    public int score;
    public bool takeDamage = true;
    public SpriteRenderer _sprite = null;

    //vector3 position= new vector3(-17.563,-1,0);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>();
    }

    public void PickStar(int sc)
    {
        score += sc;
    }

    

    // Update is called once per frame
    public void TakingDamage(float damage)
    {
        if (takeDamage == false)
        {
            health -= damage;
            if (health <= 0)
            {
                player.SetActive(false);
                player.transform.position = respawn;
                player.SetActive(true);
                health = 10;
            }
        }
               
    }
}