using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "DeleteSpike")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Joueur")
        {
            StartCoroutine(WaitForDelete());
        }
    }

    IEnumerator WaitForDelete()
    {
        yield return new WaitForEndOfFrame();
        this.gameObject.SetActive(false);
    }
}
