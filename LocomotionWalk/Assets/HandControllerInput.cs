using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerInput : MonoBehaviour {

    private OVRInput.Controller thisController;
    [SerializeField]
    private bool leftHand;
    [SerializeField]
    private GameObject player;

    //Walking
    private float walkX;
    private float walkY;
    private Vector3 movementDirection;
    public float moveSpeed = 2f;

    //Rotation
    private bool hasRotated = false;
    private float rotation;

    // Use this for initialization
    void Start () {
		if (!leftHand) {
            thisController = OVRInput.Controller.RTouch;
        }
        else {
            thisController = OVRInput.Controller.LTouch;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    // Update or fixed update?
    void FixedUpdate() {
        if (leftHand) {
            walkX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, thisController).x;
            walkY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, thisController).y;

            if (walkY > 0.70f) {
                walkForward();
            }
            else if (walkY < -0.70f) {
                walkBack();
            }

            if (walkX > 0.70f) {
                strafeRight();
            }
            else if (walkX < -0.70f) {
                strafeLeft();
            }
        }
        else {
            rotation = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, thisController).x;

            if (!hasRotated) {
                if (rotation > 0.90f) {
                    rotateRight();
                    hasRotated = true;
                }
                else if (rotation < -0.90f) {
                    rotateLeft();
                    hasRotated = true;
                }
            }

            if (rotation == 0f) {
                hasRotated = false;
            }
        }
    }

    void walkForward() {
        movementDirection = player.transform.forward;
        movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z);
        movementDirection *= Time.deltaTime / moveSpeed;
        player.transform.position += movementDirection;
    }

    void walkBack() {
        movementDirection = -player.transform.forward;
        movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z);
        movementDirection *= Time.deltaTime / moveSpeed;
        player.transform.position += movementDirection;
    }

    void strafeLeft() {
        movementDirection = Vector3.left;
        movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z);
        movementDirection *= Time.deltaTime / moveSpeed;
        player.transform.position += movementDirection;
    }

    void strafeRight() {
        movementDirection = Vector3.right;
        movementDirection = new Vector3(movementDirection.x, 0, movementDirection.z);
        movementDirection *= Time.deltaTime / moveSpeed;
        player.transform.position += movementDirection;
    }

    void rotateLeft() {
        player.transform.Rotate(new Vector3(0f, -90f, 0f));
    }

    void rotateRight() {
        player.transform.Rotate(new Vector3(0f, 90f, 0f));
    }
}
