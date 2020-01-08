using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golf_Ball : MonoBehaviour{

    [SerializeField] float lowVelocityThreshold = 5;
    [SerializeField] float maxAngularVelocity = 1f;
    [SerializeField] public float maxPower = 100;
    [SerializeField] GameController controller;
    [SerializeField] public float power = 50;
    [SerializeField] float minPower = 10;
    private Vector3 lastPosition;
    public bool canShoot = true;
    private LineRenderer lr;
    private Rigidbody rb;
    int lowVelocityCount;
    public bool isMoving;
    public float angle;
    int ballLayerMask;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        lr = gameObject.GetComponent<LineRenderer>();
        lr.sortingLayerName = "Foreground";
        rb.maxAngularVelocity = maxAngularVelocity;
        ballLayerMask = ~(1 << LayerMask.NameToLayer("Ball") | 1 << LayerMask.NameToLayer("Hole"));
        lr.endColor = new Color(1, 1, 1, 0.1f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Goal") {
            other.GetComponent<ParticleSystem>().Play();
            controller.finishTimer = 1;
            controller.FadeMusicOut();
            controller.ShowUI(false);
            controller.HoleSound();
            canShoot = false;
        }
    }
    
    private void Update() {
        DisplayGuides();
        HandleInput();
        if (!isMoving) {
            SetAngle();
        }
        //If movement speed is high, keep resetting the lowVelocity check
        if (rb.velocity.magnitude > 0.5) {
            lowVelocityCount = 0;
        }
        if (isMoving && rb.velocity.magnitude < 0.01) {
            lowVelocityCount++;
        }
        if (isMoving && lowVelocityCount > lowVelocityThreshold) {
            SetAngle();
            FreezeBall();
            DisplayGuides();
        }
        SlowBall();
        //TODO use a collider to detect when the ball is off the course?
        if(transform.position.y < -1) {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = lastPosition;
            lowVelocityCount = 0;
        }
    }

    private void SetAngle() {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    private void FreezeBall() {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.velocity = new Vector3(0, 0, 0);
        if (canShoot) {
            controller.ShowUI(true);
        }
        isMoving = false;
    }

    private void HandleInput() {
        if (Input.GetKey(KeyCode.F)) {
            angle = 0;
            //transform.rotation = Quaternion.identity;
            //transform.localRotation = Quaternion.identity;
        }
        if (Input.GetKey(KeyCode.A)) {
            angle -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            angle += 1;
        }
        if (!isMoving) {
            if (Input.GetKey(KeyCode.W)) {
                if (power < maxPower) power += 0.5f;
            }
            if (Input.GetKey(KeyCode.S)) {
                if (power > minPower) power -= 0.5f;
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb.constraints = RigidbodyConstraints.None;
                lastPosition = transform.position;
                rb.AddRelativeForce(0, 0, power);
                controller.ShowUI(false);
                controller.PuttSound();
                lowVelocityCount = 0;
                controller.shots++;
                isMoving = true;
            }
        }
    }

    private void DisplayGuides() {
        if (isMoving || !canShoot) {
            lr.positionCount = 0;
        } else {
            lr.positionCount = 2;
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + (transform.forward);
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);
            RaycastHit colliderInfo;
            Debug.DrawRay(startPos, transform.TransformDirection(Vector3.forward), Color.red, 1);
            if (Physics.Raycast(startPos, (transform.forward), out colliderInfo, 1f, ballLayerMask)) {
                lr.SetPosition(1, colliderInfo.point);
            }
        }
    }

    private void SlowBall() {
        rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(0, 0, 0), 0.01f);
    }

}
