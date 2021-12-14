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

    Vector3 startPosition;
    float initialScale;
    float scalePercentage;
    float shrinkTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.parent.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            grow = false;
            shrink = true;
            shrinkTime = 0;
            initialScale = transform.parent.transform.localScale.x;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            grow = true;
            shrink = false;
            shrinkTime = 0;
            initialScale = transform.parent.transform.localScale.x;
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
                shrinkTime += Time.deltaTime / 2;
                scalePercentage = Mathf.Lerp(initialScale, 0.01f, shrinkTime);
                transform.parent.transform.localScale = new Vector3(scalePercentage,scalePercentage,scalePercentage);
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
                shrinkTime += Time.deltaTime;
                scalePercentage = Mathf.Lerp(initialScale, 1f, shrinkTime);
                transform.parent.transform.localScale = new Vector3(scalePercentage, scalePercentage, scalePercentage);
                audioSource.clip = growSound;
                if(!audioSource.isPlaying)
                    audioSource.Play();
            }
        }    
    }

    private void _Move()
    {
        float posY = startPosition.y + (1 * Mathf.PingPong(Time.time * 0.5f, 1));

        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, posY, transform.parent.transform.position.z);
    }
}
