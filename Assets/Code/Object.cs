using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    GameObject tube;
    GameObject rotor;
    GameObject seats;

    float tubeSpeed = 15.0f;
    float rotorSpeed = 45.0f;
    float seatsSpeed = 90.0f;

    int tubeClock = 1;

    // Start is called before the first frame update
    void Start()
    {
        tube = transform.GetChild(1).transform.GetChild(0).transform.gameObject;
        rotor = tube.transform.GetChild(0).transform.gameObject;
        seats = rotor.transform.GetChild(0).transform.GetChild(0).transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        float tubeSpin = tubeClock * tubeSpeed;

        // Change direction of tube clock once it hits certain angles
        if (tube.transform.localEulerAngles.y > 30.0f || tube.transform.localEulerAngles.y < -30.0f) tubeClock *= -1;

        // Rotate things
        tube.transform.localEulerAngles += tubeSpin * transform.right * dt;
        rotor.transform.localEulerAngles += rotorSpeed * transform.up * dt;
        seats.transform.localEulerAngles += seatsSpeed * dt * transform.up;
    }
}
