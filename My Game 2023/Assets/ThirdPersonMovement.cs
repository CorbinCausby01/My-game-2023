using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        //creates input for x axis using 'A' and 'D'
        float horizontal = Input.GetAxisRaw("Horizontal");
        //creates input for z axis using 'W' and 'S'
        float vertical = Input.GetAxisRaw("Vertical");
        //creates direction based off input(x,y,z). Normalized makes diagonal movement the same.
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)//if there is input
        {
            //finds the angle between 0(default forward) to the position you are facing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Math function that smooths numbers and angles inside of Unity
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //Faces player in direction moved
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Direction we want to move in taking in account the direction of the camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //will move in the direction of input along with the set 6f speed. Time.deltatime makes framerate independent
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //update
        }
    }
}
