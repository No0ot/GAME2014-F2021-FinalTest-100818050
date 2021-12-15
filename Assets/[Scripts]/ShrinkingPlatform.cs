//      Author          : Chris Tulip
//      StudentID       : 100818050
//      Date Modified   : December 14, 2021
//      File            : ShrinkingPlatform.cs
//      Description     : This controls the the shrinking platforms
//      History         :   v0.5 - Added shrinking and floating movement
//                          v0.8 - Added Sound Effects
//                          v1.0 - Changed scaling to use lerp
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
    /// <summary>
    /// Shrinks the platform to 0.01f scale. Since lerp.t is a value between 0 and 1, divide Time.deletaTime by 2 to get rougly 2 seconds
    /// Initial scale is set on collision so that the platform can shrink if its mid grow.
    /// </summary>
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
    /// <summary>
    /// Used ping pong to make floating effect.
    /// </summary>
    private void _Move()
    {
        float posY = startPosition.y + (1 * Mathf.PingPong(Time.time * 0.5f, 1));

        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, posY, transform.parent.transform.position.z);
    }
}
