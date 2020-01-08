using UnityEngine;
using System.Collections;

public class ChangeBallLayer : MonoBehaviour {

    [SerializeField] private int LayerOnEnter; // BallInHole
    [SerializeField] private int LayerOnExit;  // BallOnTable
	
    void OnTriggerEnter(Collider other){
        //Debug.Log(gameObject.name);
        if(other.gameObject.tag == "Ball"){
            other.gameObject.layer = LayerOnEnter;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Ball"){
            other.gameObject.layer = LayerOnExit;
        }
    }
}