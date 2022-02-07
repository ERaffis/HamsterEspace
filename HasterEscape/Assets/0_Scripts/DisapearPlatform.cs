using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisapearPlatform : MonoBehaviour
{
    //Script qui fait disparaitre et apparaitre les plateformes


    private int timer;
    public int startTimer;
    public int frame1, frame2;

    void Start()
    {
        timer = startTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer ++;
        UpdatePlatform();
    }

    void UpdatePlatform()
    {
        if (timer >= frame1 && timer <= frame2)
        {
            this.gameObject.GetComponent<TilemapCollider2D>().enabled = false;
            this.gameObject.GetComponent<Tilemap>().color = Color.grey;    
        } 
        if (timer < frame1)
        {
            this.gameObject.GetComponent<TilemapCollider2D>().enabled = true;
            this.gameObject.GetComponent<Tilemap>().color = Color.white;
        }
        if (timer > frame2)
        {
            timer = 0;
        }
    }

}
