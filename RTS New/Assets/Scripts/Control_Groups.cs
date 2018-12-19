using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Groups : MonoBehaviour {

    private List<Collider> group0;
    public List<Collider> group1;
    private List<Collider> group2;
    private List<Collider> group3;
    private List<Collider> group4;
    private List<Collider> group5;
    private List<Collider> group6;
    private List<Collider> group7;
    private List<Collider> group8;
    private List<Collider> group9;
    private List<Collider> units;
    private List<Collider> noDupes;

    // Use this for initialization
    void Start()
    {

        group0 = new List<Collider>();
        group1 = new List<Collider>();
        group2 = new List<Collider>();
        group3 = new List<Collider>();
        group4 = new List<Collider>();
        group5 = new List<Collider>();
        group6 = new List<Collider>();
        group7 = new List<Collider>();
        group8 = new List<Collider>();
        group9 = new List<Collider>();
        noDupes = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {


        units = GetComponent<Selection>().selectables;
        if ((Input.GetKey("left alt") || Input.GetKey("right alt")) && GetComponent<Selection>().selectables.Count > 0)
        {

            if (Input.GetKeyDown("0"))
            {
                controlGroup(group0);
            }
            if (Input.GetKeyDown("1"))
            {
                controlGroup(group1);
            }
            if (Input.GetKeyDown("2"))
            {
                controlGroup(group2);
            }
            if (Input.GetKeyDown("3"))
            {
                controlGroup(group3);
            }
            if (Input.GetKeyDown("4"))
            {
                controlGroup(group4);
            }
            if (Input.GetKeyDown("5"))
            {
                controlGroup(group5);
            }
            if (Input.GetKeyDown("6"))
            {
                controlGroup(group6);
            }
            if (Input.GetKeyDown("7"))
            {
                controlGroup(group7);
            }
            if (Input.GetKeyDown("8"))
            {
                controlGroup(group8);
            }
            if (Input.GetKeyDown("9"))
            {
                controlGroup(group9);
            }
        }
        if ((Input.GetKey("left shift") || Input.GetKey("right shift")) && GetComponent<Selection>().selectables.Count > 0)
        {

            if (Input.GetKeyDown("0"))
            {
                clearDuplicates(group0);
            }
            if (Input.GetKeyDown("1"))
            {
                clearDuplicates(group1);
            }
            if (Input.GetKeyDown("2"))
            {
                clearDuplicates(group2);
            }
            if (Input.GetKeyDown("3"))
            {
                clearDuplicates(group3);
            }
            if (Input.GetKeyDown("4"))
            {
                clearDuplicates(group4);
            }
            if (Input.GetKeyDown("5"))
            {
                clearDuplicates(group5);
            }
            if (Input.GetKeyDown("6"))
            {
                clearDuplicates(group6);
            }
            if (Input.GetKeyDown("7"))
            {
                clearDuplicates(group7);
            }
            if (Input.GetKeyDown("8"))
            {
                clearDuplicates(group8);
            }
            if (Input.GetKeyDown("9"))
            {
                clearDuplicates(group9);
            }
        }
        else
        {

            if (Input.GetKey("0"))
            {
                select(group0);
            }
            if (Input.GetKey("1"))
            {
                select(group1);
            }
            if (Input.GetKey("2"))
            {
                select(group2);
            }
            if (Input.GetKey("3"))
            {
                select(group3);
            }
            if (Input.GetKey("4"))
            {
                select(group4);
            }
            if (Input.GetKey("5"))
            {
                select(group5);
            }
            if (Input.GetKey("6"))
            {
                select(group6);
            }
            if (Input.GetKey("7"))
            {
                select(group7);
            }
            if (Input.GetKey("8"))
            {
                select(group8);
            }
            if (Input.GetKey("9"))
            {
                select(group9);
            }
        }
    }

    void controlGroup(List<Collider> g)
    {
        g.Clear();
        for (int i = 0; i < units.Count; i++)
        {
            g.Add(units[i]);
        }
    }

    private void select(List<Collider> g)
    {
        GetComponent<Selection>().clearSelectables();

        for (int i = 0; i < g.Count; i++)
        {
            units.Add(g[i]);
            g[i].GetComponent<Selectable_Script>().selected = true;
            g[i].GetComponent<Selectable_Script>().change = true;
        }
    }

    void clearDuplicates(List<Collider> g)
    {
        if (g.Count == 0)
        {
            controlGroup(g);
        }

        for (int i = 0; i < units.Count; i++)
        {
            for (int j = 0; j < g.Count; j++)
            {
                if (units[i] == g[j])
                {
                    g.Remove(g[j]);
                    j--;
                }
            }
        }

        for (int i = 0; i < units.Count; i++)
        {
            g.Add(units[i]);
        }
    }
}
