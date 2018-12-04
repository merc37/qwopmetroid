using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private Vector3 savedPlayePosition;
    private float playerHealth;
    private float[] saveItemNumbersList;
    private float[] saveItemStackNumbersList;

    public PlayerData(Vector3 playerPosition, float playerHealth, float[] itemNumbers, float[] itemStackNumbers)
    {
        this.savedPlayePosition = playerPosition;
        this.playerHealth = playerHealth;
        SavePlayerCurrentItemRefNumbers(itemNumbers, saveItemNumbersList);
        SavePlayerItemStackNumbers(itemStackNumbers, saveItemNumbersList);
    }

    private void SavePlayerCurrentItemRefNumbers(float[] itemNumbers, float[] itemNumberSaveData)
    {
        itemNumberSaveData = new float[itemNumbers.Length];
        for (int i = 0; i < itemNumberSaveData.Length; i++)
        {
            itemNumberSaveData[i] = itemNumbers[i];
        }
    }

    private void SavePlayerItemStackNumbers(float[] itemStackNumbers, float[] itemStackNumberSaveData)
    {
        itemStackNumberSaveData = new float[itemStackNumbers.Length];
        for (int i = 0; i < itemStackNumberSaveData.Length; i++)
        {
            itemStackNumberSaveData[i] = itemStackNumbers[i];
        }
    }

    public string SavedString(string dataSeperator)
    {
        string[] saveContent = {
            "" + playerHealth,
            "" + savedPlayePosition,
            "" + saveItemNumbersList,
            "" + saveItemStackNumbersList
        };

        return string.Join(dataSeperator, saveContent);
    }
}

public class SaveData : MonoBehaviour {
    [Header("Save data combiner")]
    [SerializeField]
    private const string dataSeperator = "#SD#";
    [Space]
    [Header("Player Data To Save")]
    public FloatReference playerX;
    public FloatReference playerY;
    public CharacterItemsList playerItems;
    
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Load();
        }
    }

    private void Save()
    {

        File.WriteAllText(Application.dataPath + "/save1.txt", "save text 5");
    }

    private void Load()
    {
        string textOnSave = File.ReadAllText(Application.dataPath + "/save1.txt");
        Debug.Log(textOnSave);
    }
}
