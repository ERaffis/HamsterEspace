using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    
    [Header ("Horizontal Movement")]
    [Range(0,10)] public float speed;
    [Range(0,10)] public float sprintSpeed;

    [Header("Vertical Movement")]
    [Range(0,15)] public float jumpPower;
    [Range(0,1)] public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("System Values")]
    [Range(0,50)] public float sprintPercent;
    public Vector2 moveInput;

    [Header("Components")]
    public Rigidbody2D rb;
    public StateManager stMan;

    [Header("Physics")]
    [Range(0,10)] public float maxSpeed = 6f;
    [Range(0,10)] public float linearDrag = 8f;   
    public float gravity = 1f;
    public float fallMultiplier = 5f;
   
    //Awake & Start
    void Start()
    {
        
        sprintPercent = 50;

        rb = gameObject.GetComponent<Rigidbody2D>();
        stMan = GetComponent<StateManager>();
    }

    //Update & FixedUpdate
    void Update() 
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if(Input.GetButtonDown("Jump")) jumpTimer = Time.time + jumpDelay;
       
        stMan.CheckSprint(sprintPercent);
        stMan.PlayerStatus(stMan.isFacingRight);
    }
    void FixedUpdate()
    {
        PlayerMove(PlayerSprint());

        if(jumpTimer > Time.time && stMan.jumpCounter < 2)  PlayerJump();


        ModifyPhysics(); 
    }

    //Fonction pour le saut du personnage
    void PlayerJump()
    {
        
        stMan.jumpCounter ++;

        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jumpTimer = 0;
            
        StartCoroutine(JumpSqueeze(0.7f, 1.25f, 0.15f));    
    }
    
    //Fonction pour le mouvement du personnage
    void PlayerMove(float currentSpeed)
    {   
        
        rb.AddForce(Vector2.right * moveInput * currentSpeed);

        if(Input.GetButton("Fire3")) maxSpeed = 10f;
        if(!Input.GetButton("Fire3")) maxSpeed = 6f;

        if (moveInput.x > 0.5f)
        {
            stMan.isFacingRight = true;
        } else if (moveInput.x < -0.5f)
        {
            stMan.isFacingRight = false;
        }

        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }        
    }

    //Fonction qui modifie la vitesse du joueur pour le sprint
    float PlayerSprint()
    {
        if(stMan.canSprint == true && Input.GetButton("Fire3"))
        {   
            
            if (sprintPercent > 0)
            {
                sprintPercent -= 0.75f;
                stMan.UI.sprintSlider.value = sprintPercent; 
            }

            return speed*1.25f;
        }

        if(stMan.canSprint == false || !Input.GetButton("Fire3"))
        {
            if (sprintPercent < 50)
            {
                sprintPercent += 0.25f;
                stMan.UI.sprintSlider.value = sprintPercent; 
            }

            return speed;
        }

        return speed;
    }

    //Fonction qui modifie le frottement lineaire selon certain parametres
    void ModifyPhysics()
    {
        bool changingDir = (moveInput.x > 0 && rb.velocity.x < 0) || (moveInput.x < 0 && rb.velocity.x >0);

        if(stMan.grounded)
        {
            if(Mathf.Abs(moveInput.x) < 0.25f || changingDir  )
            {
                rb.drag = linearDrag;
            } else
            {
                rb.drag = 0f;
            }

            rb.gravityScale = 0f;
        } else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag *0.15f;

            if(rb.velocity.y < 0)
            {
                rb.gravityScale = gravity *fallMultiplier;
            } else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier/2);
            }
        }
    }

    //Fonction qui modifie la Scale du joueur lorsqu'il saute
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) 
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            this.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            this.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }

}
