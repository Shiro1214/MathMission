using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 200;
    public GameObject player;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPosition, 0.125f); 
        // bacically current pos = current pos + (end-pos - curpos) * fraction(speed);
        transform.position = smoothedPos; 
        // Move focal point with player

    }

}

