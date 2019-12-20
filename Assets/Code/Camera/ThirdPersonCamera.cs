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
    float zoomMaxSpeed = 2.0f;
    float zoomMaxAccel = 0.5f;

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

        transform.rotation = updateCameraRotation();

        transform.LookAt(target.transform.position + focusedPoint);

        transform.position = target.transform.position + cameraPosition.toCarthesian();
    }
    #endregion

    #region Methods

    Quaternion updateCameraRotation()
    {
        Quaternion ret = new Quaternion();

        ret = Quaternion.LookRotation(-transform.position, Vector3.up);

        return ret;
    }

    void updateSphericalPosition(float delta, Vector2 inputs)
    {
        float horAngleOffset = delta * inputs.x;
        float verAngleOffset = delta * inputs.y;
        cameraPosition.Theta += verAngleOffset;
        cameraPosition.Phi += horAngleOffset;
    }

    void updateDistanceToTarget(float delta)
    {
        Camera thisCamera = GetComponent<Camera>();

        float fovIncrement = 0.25f;

        if (Input.GetKey(KeyCode.Z))
        {
            cameraPosition.Distance -= zoomMaxSpeed * delta;

            thisCamera.fieldOfView += fovIncrement;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            cameraPosition.Distance += zoomMaxSpeed * delta;

            thisCamera.fieldOfView -= fovIncrement;
        }
    }

    #endregion
}
