using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("Globales Maximalgewicht Setzen")]
    [SerializeField]
    private InputField inGlobalesMaximalgewicht;
    [SerializeField]
    private Button btnGlobalesMaximalgewichtSetzen;

    [Header("Adder")]
    [SerializeField]
    private InputField inWert;
    [SerializeField]
    private InputField inGewicht;
    [SerializeField]
    private Button btnAdd;

    [Header("Generator")]
    [SerializeField]
    private InputField inAnzG;
    [SerializeField]
    private InputField inWertuntergrenze;
    [SerializeField]
    private InputField inWertobergrenze;
    [SerializeField]
    private InputField inMaxGewicht;
    [SerializeField]
    private Button btnGenerate;

    [Header("Clear")]
    [SerializeField]
    private Button btnClear;

    [Header("Solver")]
    [SerializeField]
    private Button btnSolveNW;

    [Header("Prefabs")]
    [SerializeField]
    private Button btnPrefab;

    [Header("Visualizer")]
    [SerializeField]
    private Transform contentInput;
    [SerializeField]
    private Transform contentOutput;

    //Dynamische Liste mit Gegenständen
    private readonly List<Gegenstand> gegenstaende = new List<Gegenstand>();

    //Globales Maximalgewicht
    [Header("Globales Maximalgewicht")]
    [SerializeField]
    private Text txtMaxGlobalWeight;
    [SerializeField]
    private int globalMaxWeight = 1;
    
    //Diese Methode wird automatisch von Unity aufgerufen
    void Start()
    {
        btnGlobalesMaximalgewichtSetzen.onClick.AddListener(setGlobalMaxWeight);
        btnAdd.onClick.AddListener(addItem);
        btnGenerate.onClick.AddListener(generate);
        btnClear.onClick.AddListener(clearItems);
        btnSolveNW.onClick.AddListener(solve);
        InputVisualizer.set(btnPrefab, contentInput);
        OutputVisualizer.set(btnPrefab, contentOutput);
    }

    private void setGlobalMaxWeight()
    {
        globalMaxWeight = Math.Max(1, Math.Abs(int.Parse(inGlobalesMaximalgewicht.text)));
        txtMaxGlobalWeight.text = globalMaxWeight.ToString();
        inGlobalesMaximalgewicht.text = "";
    }

    private void addItem()
    {
        //Gegenstand nur dann hinzufügen wenn das angegebene Gewicht kleiner oder gleich dem globalen Maximalgewicht ist
        if(int.Parse(inGewicht.text) <= globalMaxWeight)
        {
            gegenstaende.Add(new Gegenstand(int.Parse(inWert.text), int.Parse(inGewicht.text)));
        }
        else
        {
            gegenstaende.Add(new Gegenstand(int.Parse(inWert.text), globalMaxWeight));
        }
        
        inWert.text = "";
        inGewicht.text = "";
        visualizeInput();
    }

    private void generate()
    {
        //Gegenstände nur dann mit angegebenen Gewicht hinzufügen, wenn das angegebene Gewicht kleiner oder gleich dem globalen Maximalgewicht ist
        if (int.Parse(inMaxGewicht.text) <= globalMaxWeight)
        {
            //Gegenstände generieren
            System.Random r = new System.Random();
            for (int i = 0; i < int.Parse(inAnzG.text); i++)
            {
                gegenstaende.Add(new Gegenstand(r.Next(int.Parse(inWertuntergrenze.text), int.Parse(inWertobergrenze.text) + 1), r.Next(1, int.Parse(inMaxGewicht.text) + 1)));
            }
        }
        else
        {
            //Gegenstände generieren
            System.Random r = new System.Random();
            for (int i = 0; i < int.Parse(inAnzG.text); i++)
            {
                gegenstaende.Add(new Gegenstand(r.Next(1, 100), r.Next(1, globalMaxWeight + 1)));
            }
        }
        
        inAnzG.text = "";
        inWertuntergrenze.text = "";
        inWertobergrenze.text = "";
        inMaxGewicht.text = "";
        visualizeInput();
    }

    private void clearItems()
    {
        gegenstaende.Clear();
        visualizeInput();
    }

    private void solve()
    {
        gegenstaende.Insert(0, new Gegenstand(-1, -1));

        //Problem lösen
        List<Gegenstand> loesungsGegenstaende = Solver.solve(gegenstaende, globalMaxWeight);
        loesungsGegenstaende.Reverse();

        string loesung = "Es werden folgende Gegenstände benötigt(Wert|Gewicht):";
        foreach (Gegenstand g in loesungsGegenstaende)
        {
            loesung += " (" + g.getvalue() + "|" + g.getweight() + ")";
        }
        loesung += ".";

        gegenstaende.RemoveAt(0);

        Debug.Log(loesung);
        visualizeOutput(loesungsGegenstaende);
    }

    private void visualizeInput()
    {
        InputVisualizer.clear();
        foreach (Gegenstand g in gegenstaende)
        {
            InputVisualizer.add(g.getvalue(), g.getweight());
        }
    }

    private static void visualizeOutput(List<Gegenstand> _loesungsGegenstaende) 
    {
        OutputVisualizer.clear();
        int sumvalue = 0;
        int sumweight = 0;
        foreach (Gegenstand g in _loesungsGegenstaende)
        {
            sumvalue += g.getvalue();
            sumweight += g.getweight();
            OutputVisualizer.add(g.getvalue(), g.getweight());
        }
        OutputVisualizer.total(sumvalue, sumweight);
    }
}
