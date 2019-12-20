using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLapse : MonoBehaviour
{
    // Attributes
    public float timelapseSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        Light thisLight = GetComponent<Light>();
        thisLight.transform.eulerAngles += timelapseSpeed * transform.right;
    }
}
