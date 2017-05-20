using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    [Header("Inputfields to check")]
    [SerializeField]
    private InputField[] inputs = new InputField[1];

    [Header("Button to enable")]
    [SerializeField]
    private Button btnEnter;

	void Start () {
        foreach (InputField inf in inputs)
        {
            inf.onValueChanged.AddListener(delegate { checkIfAllAreNotEmpty(); });
        }
	}

    private void checkIfAllAreNotEmpty()
    {
        bool allAreOk = true;
        foreach (InputField inf in inputs)
        {
            if(inf.text == "")
            {
                allAreOk = false;
            }
        }
        btnEnter.interactable = allAreOk;
    }
}
