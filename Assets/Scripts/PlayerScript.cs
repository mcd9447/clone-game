using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //speed number
    public float speed = 3.0f;

    public Rigidbody2D rb;
    public Animator animator;

    //sound
    public AudioSource mySource;
    public AudioClip keysound;
    public AudioClip doorsound;
    public AudioClip npcsound;
    

    Vector2 movement;

    //do you have the key?
    bool haveKey = false;

    public float finished = GameManager.finishedGame;
    bool won = false;

    public GameObject firstnpcText;
    public GameObject secondnpcText;
    public GameObject thirdnpcText;

    void Start()
    {
        firstnpcText.SetActive(false);
        secondnpcText.SetActive(false);
        thirdnpcText.SetActive(false);
    }

    void Update()
    {
        //which keys control movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //controlling animation
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (won)
        {
            finished += 1;
            SceneManager.LoadScene(0);
        }

    }

    private void FixedUpdate()
    {
        //moving player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.name == "Key")
        {
            haveKey = true;
            mySource.PlayOneShot(keysound);
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "Door" && haveKey)
        {
            
            Destroy(other.gameObject);
            mySource.PlayOneShot(doorsound);
        }

        if (other.gameObject.name == "EndBush")
        {
            won = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "orangeNPC")
        {
            //Debug.Log(other.gameObject.name);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (haveKey)
                {
                    secondnpcText.SetActive(true);
                    mySource.PlayOneShot(npcsound);
                }
                else
                {
                    firstnpcText.SetActive(true);
                    mySource.PlayOneShot(npcsound);
                }
            }
        }
      

        if (other.gameObject.name == "blueNPC")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                thirdnpcText.SetActive(true);
                mySource.PlayOneShot(npcsound);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "orangeNPC")
        {
            if (haveKey)
            {
                secondnpcText.SetActive(false);
            }
            else
            {
                firstnpcText.SetActive(false);
            }
        }

        if (other.gameObject.name == "blueNPC")
        {
            thirdnpcText.SetActive(false);
        }

    }

}
