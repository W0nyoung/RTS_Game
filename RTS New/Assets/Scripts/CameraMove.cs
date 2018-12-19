using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
            pos[1] = pos[1] + 1;
        transform.position = pos;

        if (Input.GetKey(KeyCode.DownArrow))
            pos[1] = pos[1] - 1;
        transform.position = pos;

        if (Input.GetKey(KeyCode.RightArrow))
            pos[0] = pos[0] + 1;
        transform.position = pos;

        if (Input.GetKey(KeyCode.LeftArrow))
            pos[0] = pos[0] - 1;
        transform.position = pos;
    }
}
