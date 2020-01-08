using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFlagPole : MonoBehaviour{

    Vector3 original;
    Vector3 newPos;
    bool goAway;

    private void Awake() {
        original = transform.position;
        newPos = transform.position;
        newPos.y += 0.25f;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ball") {
            goAway = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Ball") {
            goAway = false;
        }
    }

    private void Update() {
        if (goAway) {
            transform.position = Vector3.Lerp(transform.position, newPos, 0.05f);
        } else {
            transform.position = Vector3.Lerp(transform.position, original, 0.05f);
        }
    }

}
