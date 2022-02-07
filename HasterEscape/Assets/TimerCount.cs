using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerCount : MonoBehaviour
{
    [Header("Components")]
    public TMP_Text timer;


    // Update is called once per frame
    void FixedUpdate()
    {
        timer.text = Time.timeSinceLevelLoad.ToString("f2");
    }
}
