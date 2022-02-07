using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    [Header("Components")]
    public Sprite[] peanutSprite;
    public GameObject player, peanutLife;
    public StateManager st;
    public Slider sprintSlider;
    public TMP_Text scoreText;
    
    protected WaitForSecondsRealtime waitInvincibility;

    //Script qui gere le UI

    private void Awake() 
    {
        st = player.GetComponent<StateManager>();
    }

    public void ChangeHeartDown(int life)
    {
        peanutLife.GetComponent<SpriteRenderer>().sprite = peanutSprite[life-2];
    }
    public void ChangeHeartUp(int life)
    {
        peanutLife.GetComponent<SpriteRenderer>().sprite = peanutSprite[life-1];
    }

    public void ChangeScore()
    {
        st.score ++;
        scoreText.text = st.score + " / 10";
    }
}
