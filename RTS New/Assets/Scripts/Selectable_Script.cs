using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable_Script : MonoBehaviour {

    public bool selected = false;
    public int team = 0;
    private Vector3 pos;
    private Color[] color = new Color[] { Color.green, Color.red, Color.white };
    private LayerMask obstacle;
    private Rigidbody rb;
    public bool change;
    public int health;
    public int enemy;
    public bool flash;

    // Use this for initialization
    void Start()
    {
        transform.Find("pad").GetComponent<Renderer>().material.color = color[team];
        pos = transform.position;
        GetComponent<AstarAI>().targetPosition = pos;
        obstacle = 9;
        rb = GetComponent<Rigidbody>();
        change = false;
        flash = false;
        // 0 = not moving, 1 = moving, 2 = attacking
    }



    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }
        if (change)
        {
            if (selected)
            {
                transform.Find("pad").GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                transform.Find("pad").GetComponent<MeshRenderer>().enabled = false;
            }
            change = false;
        }

        if (flash)
        {
            StartCoroutine(doubleFlash());
            flash = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>().targetName == gameObject.name)
        {
            Destroy(other.gameObject);
            health -= other.GetComponent<Projectile>().damage;
        }
    }



    IEnumerator doubleFlash()
    {
        transform.Find("pad").GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(.25f);
        transform.Find("pad").GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(.25f);
        transform.Find("pad").GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(.25f);
        transform.Find("pad").GetComponent<MeshRenderer>().enabled = false;
    }


}

