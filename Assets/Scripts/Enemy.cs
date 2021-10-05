using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speedE;
    GameObject Player;
    Animator anim;
    int vida;
    bool isAlive = true;
   



    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        vida = 100;
       
    }
    void Update()
    {
        if (Player != null && isAlive == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speedE * Time.deltaTime);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {

            vida = vida -= 25;
           

            if (vida == 0)
            {
                
                anim.SetTrigger("Dead");
                isAlive = false;
                Destroy(gameObject, 0.3f);
                PlayerController.ScoreValue += 1;
            }
           

        }
       

        
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Winner");
            PlayerController.ScoreValue = 0;
        }
        
    }
}

        
  