using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : MonoBehaviour
{   

    public float speed;
    public Vector3 startPosition;
    
    private void Start() {
        startPosition = this.gameObject.transform.position;
    }

    private void Update() 
    {
      this.gameObject.transform.Translate(speed*Time.deltaTime,0,0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Collision avec d'autre enemi
        if(other.gameObject.layer == 10)
        {
            speed = speed * -1;
            this.gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x * -1,1,1);
        } 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Collision avec la fin de la plateforme
        if(other.tag == "PlatformEnd")
        {
            speed = speed * -1;
            this.gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x * -1,1,1);
        }
    }
}
