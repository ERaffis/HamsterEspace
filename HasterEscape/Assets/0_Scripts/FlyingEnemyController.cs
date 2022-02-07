using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    //Script pour les Flying_Enemy qui leur permet de chasser le joueur et de revenir au depart

    public float speed;
    public Transform cible;
    public Vector3 home;
    public bool canChace;

    private void Start() 
    {
        canChace = false;
        home = this.transform.position;
    }

    private void FixedUpdate() {
        if (canChace)
        {
            ChasePlayer();
        }
        if(!canChace)
        {
            ReturnHome();
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(this.transform.position,cible.transform.position,speed*Time.deltaTime);
    }

    private void ReturnHome()
    {
        transform.position = Vector3.MoveTowards(this.transform.position,home,speed*Time.deltaTime);
    }
}
