using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Vector3 pos;
    public Vector3 dir;

    // Use this for initialization
    public void Move(Vector3 dir)
    {
        pos = dir;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 v = new Vector3(0, 5, 0);
        Vector3 x = new Vector3(0, -1, 0);
        if (Physics.Raycast((transform.position + v), x, out hit, Mathf.Infinity))
        {
            Vector3 g = new Vector3(GetComponent<Rigidbody>().position.x, hit.point.y + (GetComponent<BoxCollider>().bounds.size.y / 2), GetComponent<Rigidbody>().position.z);
            GetComponent<Rigidbody>().position = g;
        }

        if (pos.magnitude != 0)
        {

            dir = pos * GetComponent<Selectable_Unit>().speed * Time.fixedDeltaTime;
            if (Physics.Raycast(transform.position, pos, (float)(GetComponent<BoxCollider>().bounds.size.y / 1.5), 1 << 9))
            {
                dir *= -1;
                GetComponent<AstarAI>().currentWaypoint = GetComponent<AstarAI>().path.vectorPath.Count;
            }
            if (dir.magnitude < .375)
            {
                dir *= 0;
            }
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + dir);
            pos *= 0;
        }
    }

    // Update is called once per frame

}
