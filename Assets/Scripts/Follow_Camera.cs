using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Camera : MonoBehaviour{

    [SerializeField] private Transform target;
    [SerializeField] private Golf_Ball ball;
    [SerializeField] public float distance = 1f;
    [SerializeField] public float yOffset = 1f;

    private void FixedUpdate() {
        if (target == null) {
            Debug.LogWarning("Missing target variable on object:", this);
            return;
        }
        transform.rotation = Quaternion.Euler(0, ball.angle, 0);
        Vector3 current = ball.transform.position - transform.TransformDirection(Vector3.forward * distance);
        current.y += yOffset;
        transform.position = current;
        transform.LookAt(ball.transform);
    }

}
