using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPalace : MonoBehaviour {

	public BoolVariable accessMemory;
    public GameObject memoryPanel;

	// Use this for initialization
	void Start () {
		
	}

    void LateUpdate () {
		if(accessMemory.boolState == true)
        {
            memoryPanel.SetActive(!memoryPanel.activeSelf);
            accessMemory.boolState = false;
        }
	}
}
