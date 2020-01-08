using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Wall : MonoBehaviour {

    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;
    [SerializeField] private int waitTime = 150;
    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private bool waitOnAwake;
    private int waited;
    private Vector3 target;

    private void Awake() {
        target = position1.position;
        if (!waitOnAwake) {
            waited = waitTime;
        }
    }

    public void Update() {
        if (waited < waitTime) {
            waited++;
            return;
        }
        if (Vector3.Distance(transform.position, position1.position) < 0.01f) {
            target = position2.position;
            waited = 0;
        }
        if (Vector3.Distance(transform.position, position2.position) < 0.01f) {
            target = position1.position;
            waited = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
    }

}
