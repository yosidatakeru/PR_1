using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HPScript hpScript;

    public float goalLine = 3300f;
    public float forwardSpeed = 30f;
    public bool moveZ = true;
    public bool isGoal = false;

    private bool isControlEnabled = true;
    private PlayerMovement movement;

    public PlayerAcceleration playerAcceleration;

    void Start()
    {
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
        movement = GetComponent<PlayerMovement>();
        playerAcceleration = GetComponent<PlayerAcceleration>();
    }

    void Update()
    {
        if (isGoal || transform.position.z <= 50f ||
            transform.position.z >= goalLine || hpScript.Gauge <= 0)
        {
            isControlEnabled = false;
        }
        else
        {
            isControlEnabled = true;
        }

        if (!isControlEnabled)
        {
            movement.StopMovement();

            if (playerAcceleration != null)
            {
                playerAcceleration.isAccelerating = false;
            }

            if (isGoal)
            {
                movement.RotationReset();
            }

            return;
        }

        movement.HandleMoveAndRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            if (isGoal) return;

            isGoal = true;

            movement.StopMovement();

            if (playerAcceleration != null)
            {
                playerAcceleration.isAccelerating = false;
            }

            movement.RotationReset();

            Debug.Log("Goal‚ÉÕ“Ë‚µ‚Ü‚µ‚½I");
        }
    }

    internal bool IsControlEnabled()
    {
        return isControlEnabled;
    }
}