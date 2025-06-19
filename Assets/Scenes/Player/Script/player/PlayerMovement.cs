using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerSpeed = 1f;
    float maxSpeed = 25f;
    float damping = 1f;

    private Vector3 velocity;
    private Vector3 playerRotation;
    private Vector3 targetRotation;
    private float rotationSpeed = 5f;
   
   
    public void HandleMoveAndRotation()
    {
        HandleInput();
        UpdateMovement();
        UpdateRotation();
    }

    void HandleInput()
    {
        float inputX = Input.GetAxis("L_Stick_H");
        float inputY = Input.GetAxis("L_Stick_V");

        if (Input.GetKey(KeyCode.D)) inputX += 1f;
        if (Input.GetKey(KeyCode.A)) inputX -= 1f;
        if (Input.GetKey(KeyCode.W)) inputY += 1f;
        if (Input.GetKey(KeyCode.S)) inputY -= 1f;

        Vector3 input = new Vector3(inputX, inputY, 0f);
        if (input.magnitude > 1f) input.Normalize();

        velocity += input * playerSpeed;
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        velocity = Vector3.Lerp(velocity, Vector3.zero, damping * Time.deltaTime);
    }

    void UpdateMovement()
    {
        Vector3 newPosition = transform.position + velocity * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -34.8f, 34.8f);
        newPosition.y = Mathf.Clamp(newPosition.y, -5f, 54f);
        transform.position = newPosition;
    }

    void UpdateRotation()
    {
        float maxPitchAngle = 35f;
        float targetPitch = -velocity.y / maxSpeed * maxPitchAngle;
        targetRotation.x = Mathf.Lerp(targetRotation.x, targetPitch, Time.deltaTime * rotationSpeed);

        float maxRollAngle = 35f;
        float targetRoll = -velocity.x / maxSpeed * maxRollAngle;
        targetRotation.z = Mathf.Lerp(targetRotation.z, targetRoll, Time.deltaTime * rotationSpeed);

        playerRotation = Vector3.Lerp(playerRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z);
    }

    public void StopMovement()
    {
        velocity = Vector3.zero;
    }

    public void RotationReset() 
    {
        playerRotation = Vector3.zero;
        targetRotation = Vector3.zero;
    }

}
