using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    
    [Header ("Components")]
    public Transform cible;
    public Transform playerCible;
    public Vector3 distance;
    private Vector3 positionSmooth;
    private Vector3 positionFinale;
    
    [Header("Valeurs")]
    [Range(0,1)] public float vitesseSmooth;
    public float endSize;

    
    private void Start() 
    {
        playerCible = cible;
    }

    private void Update() 
    {   
        CameraLerp();
    }


    //Calcul du Lerp et application à la caméra
    private void CameraLerp()
    {
        positionFinale = cible.position + distance;
        positionSmooth = Vector3.Lerp(transform.position,positionFinale,vitesseSmooth);
        transform.position = positionSmooth;  
    }
}
