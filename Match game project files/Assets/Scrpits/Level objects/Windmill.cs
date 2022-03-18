using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
	public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0, RotationSpeed));
    }
}
