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
    public AudioClip finishsound;

    Vector2 movement;

    //do you have the key?
    bool haveKey = false;

    public GameObject firstnpcText;
    public GameObject secondnpcText;
    public GameObject thirdnpcText;

    void Start()
    {
        firstnpcText.SetActive(false);
        secondnpcText.SetActive(false);
        //audioSource = GetComponent<AudioSource>();
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

    }

    private void FixedUpdate()
    {
        //moving player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
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
            SceneManager.LoadScene(0);
            mySource.PlayOneShot(finishsound);
        }
    }

   void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "orangeNPC")
        {
            if (haveKey)
            {
                thirdnpcText.SetActive(false);
            }
            else
            {
                thirdnpcText.SetActive(false);
            }
        }

        if (other.gameObject.name == "blueNPC")
        {
            thirdnpcText.SetActive(false);
        }

    }

}
