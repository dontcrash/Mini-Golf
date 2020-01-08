using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling_Pin : MonoBehaviour{

    [SerializeField] AudioClip pinSound;
    AudioSource audioSource;
    int hitTimer = 0;

    public void Awake() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update() {
        if (hitTimer > 0) hitTimer--;
    }

    private void OnCollisionEnter(Collision collision) {
        if (hitTimer > 0) return;
        if(collision.transform.tag == "Ball" || collision.transform.tag == "Bowling Pin") {
            audioSource.PlayOneShot(pinSound);
            hitTimer = 10;
        }
    }

}
