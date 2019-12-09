using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    #region Attributes

    // Public
    [Header("Camera Values")]
    public GameObject target;
    public float distToTarget = 2.0f;
    public float horizontalAngle = 0.0f;
    public float verticalAngle = 75.0f;
    public Vector3 focusedPoint;
    public float horizontalSpeed = 45.0f;
    public float verticalSpeed = 45.0f;

    // Private
    private SphericalPosition cameraPosition;
    float zoomMaxSpeed = 5.0f;
    float zoomMaxAccel = 1.0f;

    #endregion

    #region Monobehaviour
    void Start()
    {
        cameraPosition.Theta = verticalAngle;
        cameraPosition.Phi = horizontalAngle;
        cameraPosition.Distance = distToTarget;

        transform.position = target.transform.position + cameraPosition.toCarthesian();
    }

    void Update()
    {
        float dt = Time.deltaTime;

        Vector2 cameraInputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        updateDistanceToTarget(dt);

        updateSphericalPosition(dt, cameraInputs);

        transform.LookAt(target.transform.position + focusedPoint);

        transform.position = target.transform.position + cameraPosition.toCarthesian();
    }
    #endregion

    #region Methods

    void updateSphericalPosition(float delta, Vector2 inputs)
    {
        float horAngleOffset = delta * inputs.x;
        float verAngleOffset = delta * inputs.y;
        cameraPosition.Theta += verAngleOffset;
        cameraPosition.Phi += horAngleOffset;
        //cameraPosition.Distance = distToTarget;
    }

    void updateDistanceToTarget(float delta)
    {
        if (Input.GetKey(KeyCode.Z))
        {
            cameraPosition.Distance -= zoomMaxSpeed * delta + zoomMaxAccel * delta * delta;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            cameraPosition.Distance += zoomMaxSpeed * delta + zoomMaxAccel * delta * delta;
        }
    }
    #endregion
}
