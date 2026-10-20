using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform DollyTrack;
    public float playerSpeed = 1f;
    float maxSpeed = 20f;
    float damping = 1f;
    public float forwardSpeed = 0f;
    private Vector3 velocity;
    private Vector3 playerRotation;
    private Vector3 targetRotation;
    private float rotationSpeed = 5f;
    private HPScript hpScript;

    void Start()
    {
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();

    }


    void Update()
    {
        

        if (hpScript.Gauge < 1)
        {
            forwardSpeed = 0;
            playerSpeed = 0;
        }
      //  HandleMoveAndRotation();
    }
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

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

        // ÉJÉÅÉâÇÃå¸Ç´ÇäÓèÄÇ…à⁄ìÆï˚å¸ÇåvéZ
        Transform cam = Camera.main.transform;

        Vector3 moveDirection =
            cam.right * input.x +
            cam.up * input.y;

        // à⁄ìÆë¨ìxÇâ¡éZ
        velocity += moveDirection * playerSpeed;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // èôÅXÇ…å∏ë¨
        velocity = Vector3.Lerp(
            velocity,
            Vector3.zero,
            damping * Time.deltaTime
        );
    }

    void UpdateMovement()
    {
        Vector3 newPosition = transform.position + velocity * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, DollyTrack.transform.position.x - 15f, DollyTrack.transform.position.x + 15f);
        newPosition.y = Mathf.Clamp(newPosition.y, DollyTrack.transform.position.y - 10f, DollyTrack.transform.position.y +10f);
        newPosition.z = Mathf.Clamp(newPosition.z, DollyTrack.transform.position.z - 15f, DollyTrack.transform.position.z + 15f);
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

        Quaternion localTilt = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z);

        transform.rotation = DollyTrack.rotation * localTilt;
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
