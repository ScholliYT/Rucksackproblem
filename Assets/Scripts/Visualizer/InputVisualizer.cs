using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputVisualizer : MonoBehaviour {

    private static Button btnPrefab = null;
    private static Transform content = null;

    public static void set(Button b, Transform t)
    {
        btnPrefab = b;
        content = t;
    }

    public Button getBtnPrefab()
    {
        return btnPrefab;
    }

    public Transform getContent()
    {
        return content;
    }


    public static void clear()
    {
        int childs = content.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.GetChild(i).gameObject);
        }
    }

    public static void add(int value, int weight)
    {
        if (!(btnPrefab == null || content == null))
        {
            Instantiate(btnPrefab, content).GetComponentInChildren<Text>().text = "Gegenstand: Wert(" + value + ") Gewicht(" + weight + ")";
        }
    }
}
