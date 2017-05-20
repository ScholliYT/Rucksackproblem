using UnityEngine.UI;
using UnityEngine;

public class OutputVisualizer : MonoBehaviour
{
    private static Button btnPrefab = null;
    private static Transform content = null;

    public static void set(Button b, Transform t)
    {
        btnPrefab = b;
        content = t;
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

    public static void total(int sumvalue, int sumweight)
    {
        if (!(btnPrefab == null || content == null))
        {
            Button b = Instantiate(btnPrefab, content);
            b.GetComponentInChildren<Text>().text = "Gesamt: Wert(" + sumvalue + ") Gewicht(" + sumweight + ")";
            b.image.color = Color.green;
        }
    }
}
