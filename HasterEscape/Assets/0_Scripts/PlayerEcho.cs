using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEcho : MonoBehaviour
{

    //Script qui fait les effets derriere le joueur

    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject echo;
    private CharacterController2D player;

    void Start()
    {
        player = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.rb.velocity != Vector2.zero)
        {
            if (timeBtwSpawns <= 0)
            {
                GameObject instance = (GameObject)Instantiate(echo,transform.position,Quaternion.identity);
                Destroy(instance,4f);
                timeBtwSpawns = startTimeBtwSpawns;
            } else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
