using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{

    [Header("PlatformSFX")]
    public AudioSource[] AUsource;

    bool shrinking;
    Vector2 StartSize; 


    private void Start()
    {
        StartSize = gameObject.transform.localScale;
        shrinking = false;
    }



    void Update()
    {
        // move the platform up and down
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0,1,0) , Mathf.PingPong(Time.time, 1));

        
        // shrink or resize on player contact
        if (shrinking)
        {
            Shrink();
            Debug.Log("Shrink");
        }
        else
        {
            Resize();
            Debug.Log("resize");

        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            shrinking = true;
            AUsource[0].Play();

        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("player detect");
            shrinking = true;

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
        if(other.transform.tag == "Player")
        { 
            shrinking = false;
            AUsource[1].Play();

        }
    }


    public void Shrink()
    {

        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.01f,0.01f), 2.0f * Time.deltaTime);
    }

    public void Resize()
    {
        
        transform.localScale = Vector2.Lerp(transform.localScale, StartSize, 2.0f * Time.deltaTime);

    }


    IEnumerator StartShrink()
    {
        
        AUsource[0].Play();
        Shrink();
        yield return new WaitForSeconds(1);
        

    }

    IEnumerator StartResize()
    {
        
        AUsource[1].Play();
        yield return new WaitForSeconds(2);
        Resize();
        

    }

    

}
