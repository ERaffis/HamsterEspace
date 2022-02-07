using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StateManager : MonoBehaviour
{
    
    //Script en lien avec tout les status du joueur

    [Header("Status")]
    public bool isFacingRight;
    public bool canSprint;
    public bool grounded;

    [Header("Counters")]
    public int jumpCounter;
    public int life = 3;
    public int score;

    [Header("Components")]
    public HudManager UI;
    public Vector3 resetPosition;
    public Camera mainCam;
    public GameObject dropCamCible;
    public LayerMask groundLayer;

    [Header("Sprites")]
    public Sprite[] hamSprites;
    public SpriteRenderer hamCurrentSprite;

    [Header("Trail")]
    public PlayerEcho echo;

    [Header("Raycast")]
    public float groundLenght;
    public Vector3 raycastOffset;
    

    //Fonction Awake & Start
    private void Start() 
    {
        
        grounded = true;
        isFacingRight = true;
        resetPosition = this.transform.position;    

        echo = GetComponent<PlayerEcho>();
    }

    private void Update() {
        grounded = Physics2D.Raycast(transform.position + raycastOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - raycastOffset, Vector2.down, groundLenght, groundLayer);
    }

    public void Dead()
    {
        SceneManager.LoadScene("DeathScene");
    }
    public void Win()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void ResestEntities(Collision2D enemy)
    {
        GetComponent<Transform>().position = resetPosition;
        mainCam.GetComponent<CameraFollow2D>().cible = this.transform;
        enemy.gameObject.GetComponent<Transform>().position = enemy.gameObject.GetComponent<GroundEnemyController>().startPosition;
    }

    public void ResestEntities()
    {
        this.GetComponent<Transform>().position = resetPosition;
    }

    public void PlayerStatus(bool faceDir)
    {
        if(faceDir == true)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if(faceDir == false)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void CheckSprint(float numb)
    {
        if(numb == 0 || !grounded) canSprint = false;
        if(numb == 50 && grounded) canSprint = true;
        
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + raycastOffset,transform.position + raycastOffset + Vector3.down * groundLenght);
        Gizmos.DrawLine(transform.position - raycastOffset,transform.position - raycastOffset + Vector3.down * groundLenght);
    }
    
}
