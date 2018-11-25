using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementIndicatorUI : MonoBehaviour {

    [SerializeField] GameObject movementIndicatorsParent;

    [SerializeField] Image[] movementIndicators;

    public FloatReference[] currentMoves;
    [SerializeField] float[] totalCurrentMoves;

    private void OnValidate()
    {
        if(movementIndicatorsParent != null)
        {
            movementIndicators = movementIndicatorsParent.GetComponentsInChildren<Image>();
        }
        totalCurrentMoves = new float[currentMoves.Length];
        for (int i = 0; i < totalCurrentMoves.Length; i++)
        {
            totalCurrentMoves[i] = currentMoves[i].Value;
        }
        for (int i = 0; i < movementIndicators.Length; i++)
        {
            if(totalCurrentMoves[i] != 0)
            {
                movementIndicators[i].fillAmount = currentMoves[i].Value / totalCurrentMoves[i];
            }
            else
            {
                movementIndicators[i].fillAmount = 0;
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < movementIndicators.Length; i++)
        {
            movementIndicators[i].fillAmount = currentMoves[i].Value / totalCurrentMoves[i];
        }
    }
}
