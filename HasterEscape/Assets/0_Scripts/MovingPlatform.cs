using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Script qui controlle les plateformes mobiles

    public Vector3 positionA, positionB;
    public float moveSpeed;
    private bool isGoingUp;
    public bool shouldMove;

    // Start is called before the first frame update
    void Start()
    {
        shouldMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlatform();
    }

    void movePlatform()
    {
        if(shouldMove)
        {
        
        if(transform.position == positionB) isGoingUp = false;

        if(transform.position == positionA) isGoingUp = true; 


        if(isGoingUp) transform.position = Vector2.MoveTowards(transform.position,positionB,moveSpeed);
        
        if (!isGoingUp) transform.position = Vector2.MoveTowards(transform.position,positionA,moveSpeed);
        
        }
        
    }
}
