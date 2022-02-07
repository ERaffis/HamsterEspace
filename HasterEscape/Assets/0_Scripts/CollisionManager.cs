using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public StateManager stMan;
    public GameObject finalDoor;
    public float groundTimer;


    private void Start() {
        stMan = GetComponent<StateManager>();
    }

    private void Update() 
    {
        groundTimer ++;
        CheckGround();
    }

    //Regarde si le joueur touche toujours au sol à la fin du frame
    void CheckGround()
    {
        if (stMan.grounded && groundTimer >= 60) 
        {
            stMan.jumpCounter = 0;
            groundTimer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        //Collision avec Layer Enemies
        if ( other.gameObject.layer == 10)
        {
            foreach(ContactPoint2D hitPos in other.contacts)
            {
                
                if(hitPos.normal.y  > .70)
                {
                    stMan.UI.ChangeScore();
                    this.GetComponent<Rigidbody2D>().AddForce(Vector2.up*7.5f, ForceMode2D.Impulse);
                    StartCoroutine(DeleteEnemy(other));
                    other.gameObject.SetActive(false);
                    break;
                }
                else
                {
                    if ( stMan.life > 1)
                    {
                        if(other.gameObject.tag == "Ground_Enemy") stMan.ResestEntities(other);
                        if(other.gameObject.tag == "Flying_Enemy") stMan.ResestEntities();            
                        stMan.UI.ChangeHeartDown(stMan.life);
                        stMan.life --;
                        stMan.hamCurrentSprite.sprite = stMan.hamSprites[stMan.life-1];
                        
                        break;
                        
                    } else
                    {
                        stMan.Dead();
                    }
                }

            }       
        }                  
        
        //Collision avec Layer Spikes & Acid && Bubbles && Falling Spikes
        if(other.gameObject.layer == 9 || other.gameObject.layer == 12 || other.gameObject.layer == 13) 
        {
            foreach(ContactPoint2D hitPos in other.contacts)
            {
                
                if ( stMan.life > 1)
                {
                    
                    stMan.ResestEntities();
                    stMan.UI.ChangeHeartDown(stMan.life);
                    stMan.life --;
                    stMan.hamCurrentSprite.sprite = stMan.hamSprites[stMan.life-1];
                    
                    break;
                } else
                {
                    stMan.Dead();
                }
            }
        }

        //Lorsque le joueur saute sur une plateforme "Mobile" ajoute la plateforme Parent
        if(other.gameObject.tag == "Mov_Platform") 
        {
            this.transform.SetParent(other.transform);
            other.gameObject.GetComponent<MovingPlatform>().shouldMove = true;  
        }

        if(other.gameObject.tag == "Mov_Platform_2") 
        {
            this.transform.SetParent(other.transform);
            other.gameObject.GetComponent<MovingPlatform>().shouldMove = true;  
        }              
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        //Lorsque le joueur saute hors d'une plateforme "Mobile" enleve la plateforme Parent
        if(other.gameObject.tag == "Mov_Platform") this.transform.SetParent(null); 
    }

    void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.gameObject.layer == 13)
        {
            other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        //Trigger avec le drapeau checkpoint
        if(other.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint Acquired");
            other.gameObject.SetActive(false);
            stMan.resetPosition = other.transform.position;
            if(stMan.life < 3)
            {
                stMan.life += 1;
                stMan.UI.ChangeHeartUp(stMan.life);
                stMan.hamCurrentSprite.sprite = stMan.hamSprites[stMan.life-1];
                
            }
            
        }

        //Trigger pour ouvrir la derniere porte
        if(other.gameObject.tag == "OpenDoor")
        {
            if(stMan.score == 10)
            {
                finalDoor.SetActive(false);
            }
        } 

        //Trigger pour la boulle
        if(other.gameObject.tag == "Victory")
        {
            stMan.Win();
        }

        //Trigger au debut de la chute pour modifier la camera
        if(other.gameObject.tag == "DropEnter")
        {
            stMan.mainCam.GetComponent<CameraFollow2D>().cible = stMan.dropCamCible.transform;
            stMan.mainCam.orthographicSize = 12.75f;
        }
        //Trigger a la fin de la chute
        if(other.gameObject.tag == "DropExit")
        {
            stMan.mainCam.GetComponent<CameraFollow2D>().cible = stMan.mainCam.GetComponent<CameraFollow2D>().playerCible.transform;
            stMan.mainCam.orthographicSize = stMan.mainCam.GetComponent<CameraFollow2D>().endSize;
            stMan.jumpCounter = 0;
        }

        //Trigger au debut de la chute pour empecher le joueur de sauter en tombant
        if(other.gameObject.tag == "DisableJump")
        {
            stMan.jumpCounter = 2;
        }

    }
    
    private void OnTriggerStay2D(Collider2D other) 
    {
        //Trigger de vision pour "Flying_Enemy" pour chasser
        if(other.gameObject.tag == "Flying_Enemy")
        {
            other.GetComponent<FlyingEnemyController>().canChace = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        //Trigger de vision pour "Flying_Enemy" pour retourner au point de depart
        if(other.gameObject.tag == "Flying_Enemy")
        {
            other.GetComponent<FlyingEnemyController>().canChace = false;
        }
    }

    IEnumerator DeleteEnemy(Collision2D other)
    {
        other.transform.localScale = Vector3.Lerp(other.transform.localScale, new Vector3(0,0,0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        
    }
}
