using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Light : MonoBehaviour{

    [SerializeField] private Transform target;

    void Update(){
        transform.position = new Vector3(target.position.x, target.position.y + 1, target.position.z);
    }
}
