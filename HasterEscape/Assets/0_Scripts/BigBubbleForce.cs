using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBubbleForce : MonoBehaviour

{

    public int frameCounter;
    public int timer;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        frameCounter = Random.Range(100,200);
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter ++;
        if(frameCounter > timer) 
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
            frameCounter = 0;
        }
    }


}
