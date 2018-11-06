using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour {

    public FloatReference playerHealth;
    [SerializeField] int numberOfHEartsVisible;

    public Transform heartsParent;
    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void OnValidate()
    {
        hearts = heartsParent.GetComponentsInChildren<Image>();
    }

    private void Update()
    {

        if(playerHealth.Value > numberOfHEartsVisible)
        {
            playerHealth.Variable.Value = numberOfHEartsVisible;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < playerHealth.Value)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numberOfHEartsVisible)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

}
