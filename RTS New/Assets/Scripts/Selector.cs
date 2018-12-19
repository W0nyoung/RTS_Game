using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    public List<Collider> selectables;
    public GameObject click;
    private Vector3 selection_start;
    private Vector3 start_Mouse;
    private Transform box;
    private Vector2 startBoxPos;
    private Vector2 endBoxPos;
    private bool drag;
    public List<GameObject> units;
    private LayerMask ground;
    private bool oneClick;
    private float delay;
    private float currentTime;
    private bool A_Selected;
    public Texture2D attackMouse;
    private Texture2D texture;
    public Texture2D selectTexture;

    // Use this for initialization
    void Start()
    {

        selectables = new List<Collider>();
        box = transform.Find("Selection Box");
        texture = new Texture2D(1, 1);
        drag = false;
        units = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        ground = 8;
        oneClick = false;
        delay = .25f;
        A_Selected = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && A_Selected == true)
        {
            RaycastHit shot;
            Ray shoot = Camera.main.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(shoot, out shot, Mathf.Infinity))
            {

                if (shot.collider.gameObject.tag == "Unit" || shot.collider.gameObject.tag == "Building")
                {
                    shot.collider.gameObject.GetComponent<Selectable_Script>().flash = true;
                    for (int i = 0; i < selectables.Count; i++)
                    {
                        selectables[i].GetComponent<Selectable_Unit>().target = shot.collider.gameObject;
                        selectables[i].GetComponent<Selectable_Unit>().idle = false;
                        selectables[i].GetComponent<Selectable_Unit>().forceAttack = true;
                        selectables[i].GetComponent<AstarAI>().targetPosition = shot.point;
                        selectables[i].GetComponent<Selectable_Unit>().priority = 2;

                    }
                }
                else
                {
                    GameObject tap = Instantiate(click, shot.point, Quaternion.identity);
                    tap.GetComponent<MeshRenderer>().enabled = true;
                    Destroy(tap, 0.25f);
                    for (int i = 0; i < selectables.Count; i++)
                    {
                        selectables[i].GetComponent<Selectable_Unit>().idle = false;
                        selectables[i].GetComponent<Selectable_Unit>().target = null;
                        selectables[i].GetComponent<AstarAI>().targetPosition = shot.point;
                        selectables[i].GetComponent<Selectable_Unit>().priority = 1;
                        selectables[i].GetComponent<Selectable_Unit>().lastTarget = shot.point;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask ground = 1 << 8;
            LayerMask units = 1 << 10;
            Ray ray_ground = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit ground_hit;

            if (Physics.Raycast(ray_ground, out ground_hit, Mathf.Infinity, ground))
            {
                selection_start = ground_hit.point;
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Unit" || hit.collider.tag == "Building")
                {
                    if (A_Selected == false && ((!checkDuplicate(hit.collider.gameObject) || !(Input.GetKey("right shift") || Input.GetKey("left shift")))))
                    {
                        clearSelectables();
                        hit.collider.GetComponent<Selectable_Script>().selected = true;
                        hit.collider.GetComponent<Selectable_Script>().change = true;
                        selectables.Add(hit.collider);
                        doubleClick(hit.collider.gameObject);
                        reorganize();
                    }
                }
            }
            A_Selected = false;
            start_Mouse = Input.mousePosition;



        }

        if (Input.GetKey("a") && selectables.Count > 0 && selectables[0].tag != "Building")
        {
            A_Selected = true;
            Cursor.SetCursor(attackMouse, new Vector2(attackMouse.width / 2, attackMouse.height / 2), CursorMode.Auto);
        }




        if (Input.GetMouseButton(0) && A_Selected == false)
        {
            A_Selected = false;
            if (Vector2.Distance(start_Mouse, Input.mousePosition) > 10)
            {
                box.gameObject.GetComponent<MeshRenderer>().enabled = true;
                Ray ray_Mouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit_Mouse;
                if (!(Input.GetKey("right shift") || Input.GetKey("left shift")))
                {
                    clearSelectables();
                }



                drag = true;
                startBoxPos = new Vector2(start_Mouse.x, (Screen.height - start_Mouse.y));
                if (Physics.Raycast(ray_Mouse, out hit_Mouse, Mathf.Infinity, 1 << 8))
                {
                    endBoxPos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
                }

            }
        }
        else
        {
            drag = false;
        }



        if (Input.GetMouseButtonDown(1) && selectables.Count != 0)
        {
            A_Selected = false;
            RaycastHit shot;
            Ray shoot = Camera.main.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(shoot, out shot, Mathf.Infinity))
            {

                if (shot.collider.gameObject.tag == "Unit" || shot.collider.gameObject.tag == "Building")
                {
                    shot.collider.gameObject.GetComponent<Selectable_Script>().flash = true;
                    for (int i = 0; i < selectables.Count; i++)
                    {
                        selectables[i].GetComponent<Selectable_Unit>().target = shot.collider.gameObject;
                        selectables[i].GetComponent<Selectable_Unit>().idle = false;
                        selectables[i].GetComponent<AstarAI>().targetPosition = shot.point;
                        selectables[i].GetComponent<Selectable_Unit>().priority = 2;
                        selectables[i].GetComponent<Selectable_Unit>().forceAttack = false;
                    }
                }
                else
                {
                    GameObject tap = Instantiate(click, shot.point, Quaternion.identity);
                    tap.GetComponent<MeshRenderer>().enabled = true;
                    Destroy(tap, 0.25f);
                    for (int i = 0; i < selectables.Count; i++)
                    {
                        selectables[i].GetComponent<Selectable_Unit>().idle = false;
                        selectables[i].GetComponent<Selectable_Unit>().target = null;
                        selectables[i].GetComponent<AstarAI>().targetPosition = shot.point;
                        selectables[i].GetComponent<Selectable_Unit>().priority = 1;
                        selectables[i].GetComponent<Selectable_Unit>().lastTarget = shot.point;
                        selectables[i].GetComponent<Selectable_Unit>().forceAttack = false;
                    }
                }
            }

        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown("h"))
        {
            for (int i = 0; i < selectables.Count; i++)
            {
                selectables[i].GetComponent<AstarAI>().targetPosition = selectables[i].GetComponent<Selectable_Script>().transform.position;
                selectables[i].GetComponent<Selectable_Unit>().priority = 0;
            }
        }
        if (Input.GetKeyDown("escape") && A_Selected)
        {
            A_Selected = false;
        }
        if (!A_Selected)
        {
            Cursor.SetCursor(selectTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    public void clearSelectables()
    {
        if (!(Input.GetKey("right shift") || Input.GetKey("left shift")))
        {
            for (int i = 0; i < selectables.Count; i++)
            {
                selectables[i].GetComponent<Selectable_Script>().selected = false;
                selectables[i].GetComponent<Selectable_Script>().change = true;
            }

            selectables.Clear();
        }
    }

    void boxSelection()
    {
        for (int i = 0; i < units.Count; i++)
        {
            Vector3 p = Camera.main.WorldToScreenPoint(units[i].transform.position);
            if (!checkDuplicate(units[i]))
            {
                if ((p.x > startBoxPos.x && p.x < endBoxPos.x) && (p.y > startBoxPos.y && p.y < endBoxPos.y))
                {
                    units[i].GetComponent<Selectable_Script>().selected = true;
                    units[i].GetComponent<Selectable_Script>().change = true;
                    selectables.Add(units[i].GetComponent<Collider>());
                }

                if ((p.x > startBoxPos.x && p.x < endBoxPos.x) && (p.y < startBoxPos.y && p.y > endBoxPos.y))
                {
                    units[i].GetComponent<Selectable_Script>().selected = true;
                    units[i].GetComponent<Selectable_Script>().change = true;
                    selectables.Add(units[i].GetComponent<Collider>());
                }

                if ((p.x < startBoxPos.x && p.x > endBoxPos.x) && (p.y > startBoxPos.y && p.y < endBoxPos.y))
                {
                    units[i].GetComponent<Selectable_Script>().selected = true;
                    units[i].GetComponent<Selectable_Script>().change = true;
                    selectables.Add(units[i].GetComponent<Collider>());
                }

                if ((p.x < startBoxPos.x && p.x > endBoxPos.x) && (p.y < startBoxPos.y && p.y > endBoxPos.y))
                {
                    units[i].GetComponent<Selectable_Script>().selected = true;
                    units[i].GetComponent<Selectable_Script>().change = true;
                    selectables.Add(units[i].GetComponent<Collider>());
                }
            }
        }
        reorganize();
    }

    bool checkDuplicate(GameObject g)
    {
        bool dupe = false;
        for (int i = 0; i < selectables.Count; i++)
        {
            if (selectables[i].gameObject == g)
            {
                dupe = true;
            }
        }
        return dupe;
    }

    void doubleClick(GameObject g)
    {
        if (!oneClick)
        {
            oneClick = true;
            currentTime = Time.time;
        }
        else
        {
            if ((Time.time - currentTime) <= delay)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    if (g.GetComponent<Selectable_Script>().team == units[i].GetComponent<Selectable_Script>().team)
                    {
                        Vector3 p = Camera.main.WorldToScreenPoint(units[i].transform.position);
                        if ((p.x > 0 && p.x < Screen.width) && (p.y > 0 && p.y < Screen.height))
                        {
                            units[i].GetComponent<Selectable_Script>().selected = true;
                            units[i].GetComponent<Selectable_Script>().change = true;
                            selectables.Add(units[i].GetComponent<Collider>());
                        }
                    }
                }
            }
            oneClick = false;
        }
    }

    void reorganize()
    {
        List<Collider> buildings = new List<Collider>();

        for (int i = 0; i < selectables.Count; i++)
        {
            if (selectables[i].tag == "Building")
            {
                buildings.Add(selectables[i]);
                selectables.Remove(selectables[i]);
                i--;
            }
        }

        if (buildings.Count > 0)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                selectables.Add(buildings[i]);
            }
        }
    }

    void OnGUI()
    {
        Rect selection_box;
        if (drag)
        {
            GUIStyle style = new GUIStyle();

            for (int y = 0; y < texture.height; ++y)
            {
                for (int x = 0; x < texture.width; ++x)
                {
                    float r = Random.value;
                    Color color = new Color(0, 1, 0, .25f);
                    texture.SetPixel(x, y, color);
                }

                texture.Apply();


                style.normal.background = texture;
                GUI.backgroundColor = Color.green;
                selection_box = new Rect(startBoxPos.x, startBoxPos.y, (endBoxPos.x - startBoxPos.x), ((startBoxPos.y - endBoxPos.y) * -1));
                GUI.Box(selection_box, "", style);

            }
            boxSelection();

        }


    }
}

