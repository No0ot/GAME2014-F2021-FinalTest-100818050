using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
     bool shrink = false;
     bool grow = false;
    AudioSource audioSource;
    public AudioClip shrinkSound;
    public AudioClip growSound;


    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            grow = false;
            shrink = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            grow = true;
            shrink = false;
        }
    }

    private void Update()
    {
        Shrink();
        Grow();
    }

    private void FixedUpdate()
    {
        _Move();
        
    }
    private void Shrink()
    {
        if(shrink)
        {
            if (transform.parent.transform.localScale.x > 0.01)
            {
                transform.parent.transform.localScale = transform.parent.transform.localScale * 0.995f;
                audioSource.clip = shrinkSound;
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
                shrink = false;
        }
    }

    private void Grow()
    {
        if(grow)
        {
            if (transform.parent.transform.localScale.x < 1)
            {
                transform.parent.transform.localScale = transform.parent.transform.localScale * 1.005f;
                audioSource.clip = growSound;
                if(!audioSource.isPlaying)
                    audioSource.Play();
            }
            else if (transform.parent.transform.localScale.x > 1)
            {
                transform.parent.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                grow = false;
            }

        }    
    }

    private void _Move()
    {
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, Mathf.PingPong(Time.time * 0.5f, 1), transform.parent.transform.position.z);
    }
}
