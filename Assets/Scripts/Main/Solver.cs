using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Solver
{
    public static List<Gegenstand> solve(List<Gegenstand> _items, int _maxWeight)
    {
        //Wenn keine Gegenstände vorhanden sind oder das Maximalgewicht 0 ist kann hier abgebrochen werden
        if (_items.Count == 0 || _maxWeight == 0)
        {
            return new List<Gegenstand>();
        }

        //Tabelle vorbereiten
        int[][] table = initTable(_items, _maxWeight);

        //Item(Gegenstand); weight(Gewicht)
        //Über jedes Element der Tabelle iterieren
        for (int item = 1; item < table.Length; item++)
        {
            for (int weight = 1; weight < _maxWeight + 1; weight++)
            {
                //Falls das Gewicht des aktuellen Gegenstands höher ist als weight muss der Wert aus dem Feld über dem jetzigen genommen werden
                if (_items[item].getweight() > weight) {
                    table[item][weight] = table[item - 1][weight];
                }
                else {
                    int fromUpperRow = table[item - 1][weight];
                    int fromUpperRowAndCurrentObject = _items[item].getvalue() +
                                                       table[item - 1][weight - _items[item].getweight()];
                    table[item][weight] = Math.Max(fromUpperRow, fromUpperRowAndCurrentObject);
                }
            }
        }

        Debug.Log("Max Possible: " + table[table.Length - 1][_maxWeight]);

        //zuletzt noch die benutzten Gegenstände zurückverfolgen
        return findUsedItems(_items, _maxWeight, table);
    }

    private static List<Gegenstand> findUsedItems(List<Gegenstand> _items, int _maxWeight, int[][] table) {
        //Die beteiligten gegenstäde ermitteln
        List<Gegenstand> usedItems = new List<Gegenstand>();
        int item = table.Length - 1;
        int weight = _maxWeight;
        while (item > 0 && weight > 0) {
            if (table[item][weight] != table[item - 1][weight]) {
                //Gegenstand o wird benutzt
                usedItems.Add(_items[item]);
                weight = weight - _items[item].getweight();
                item--;
            }
            else {
                item--;
            }
        }
        return usedItems;
    }

    private static int[][] initTable(List<Gegenstand> _items, int _maxWeight)
    {
        //Erzeugen eines Jagged Arrays mit (objects.length + 1 x max + 1) einträgen
        int[][] table = new int[_items.Count][];
        for (int i = 0; i < table.Length; i++)
        {
            table[i] = new int[_maxWeight + 1];
        }

        for (int w = 0; w <= _maxWeight; w++)
        {
            //Zeile 0 mit 0 befüllen 
            table[0][w] = 0;
        }

        foreach (int[] row in table)
        {
            //Spalte 0 mit 0 befüllen 
            row[0] = 0;
        }
        return table;
    }
}
