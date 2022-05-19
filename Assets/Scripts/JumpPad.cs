using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float thrust;
    public bool isFirstFloor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(isFirstFloor == true)
            {
                if (collision.gameObject.GetComponent<Transform>().localPosition.y < -4.271f && collision.gameObject.GetComponent<Transform>().localPosition.y > -4.28f)
                {
                }
                else
                {
                    //collision.gameObject.GetComponentInChildren<Rigidbody2D>().simulated = false;
                    collision.gameObject.GetComponent<Character>().isThroughPlatform = true;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                if (collision.gameObject.GetComponent<Transform>().localPosition.y < -1.501f && collision.gameObject.GetComponent<Transform>().localPosition.y > -1.509f)
                {
                }
                else
                {
                    //collision.gameObject.GetComponentInChildren<Rigidbody2D>().simulated = false;
                    collision.gameObject.GetComponent<Character>().isThroughPlatform = true;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
