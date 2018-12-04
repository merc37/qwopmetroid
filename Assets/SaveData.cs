using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedPlayerData
{
    private Vector3 savedPlayePosition;
    private float playerHealth;
    private float playerMoney;
    private Vector2[] inventoryList;
    private string inventoryString = "";


    public SavedPlayerData(Vector3 playerPosition, float playerHealth, float playerMoney, Vector2[] inventoryList)
    {
        this.savedPlayePosition = playerPosition;
        this.playerHealth = playerHealth;
        this.playerMoney = playerMoney;
        SaveInventory(inventoryList);
    }

    private void SaveInventory(Vector2[] playerInventoryList)
    {
        int numOfItems = playerInventoryList.Length;
        this.inventoryList = new Vector2[numOfItems];
        for (int i = 0; i < numOfItems; i++)
        {
            this.inventoryList[i] = playerInventoryList[i];
        }
        inventoryString = ItemListToString(this.inventoryList);
    }

    private string ItemListToString(Vector2[] playerInventoryList)
    {
        string itemDataString = "";
        for (int i = 0; i < playerInventoryList.Length; i++)
        {
            itemDataString += "" + playerInventoryList[i] + ":" + "\n";
        }
        return itemDataString;
    }

    public string SavedString(string dataSeperator)
    {
        string[] saveContent = {
            "" + this.playerHealth,
            "" + this.playerMoney,
            "" + this.savedPlayePosition,
            "" + this.inventoryString
        };

        return string.Join(dataSeperator + "\n", saveContent);
    }
}

public class SaveData : MonoBehaviour {
    [Header("Save data combiner")]
    [SerializeField]
    private const string DATA_SEPERATOR = "#";
    [Space]
    [Header("Player Data To Save")]
    public FloatReference playerX;
    public FloatReference playerY;
    public FloatReference playerMoney;
    public FloatReference playerHealth;
    public CharacterItemsList playerItems;

    private Vector2 playerPosition;
    private SavedPlayerData playerData;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerPosition = new Vector2(playerX.Variable.Value, playerY.Variable.Value);
            playerData = new SavedPlayerData(playerPosition, playerHealth.Variable.Value, playerMoney.Variable.Value, playerItems.itemAndStackNumber);
            string savedString = playerData.SavedString(DATA_SEPERATOR);
            Save(savedString);
            Debug.Log("Saved");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Load();
        }
    }

    private void Save(string saveString)
    {
        File.WriteAllText(Application.dataPath + "/save1.txt", saveString);
    }

    private void Load()
    {
        string textOnSave = File.ReadAllText(Application.dataPath + "/save1.txt");
        string[] contentOfSave = textOnSave.Split(new[] { DATA_SEPERATOR }, System.StringSplitOptions.None);
        //for (int i = 0; i < contentOfSave.Length; i++)
        //{
        //    Debug.Log(contentOfSave[i]);
        //}
        SetPlayerDataFromLoad(contentOfSave);
        //Debug.Log(textOnSave);
    }

    private void SetPlayerDataFromLoad(string[] loadedStrings)
    {
        playerHealth.Variable.Value = float.Parse(loadedStrings[0]);
        playerMoney.Variable.Value = float.Parse(loadedStrings[1]);
        //Debug.Log("completed health and money moving to player Position");

        string[] playerPositionString = loadedStrings[2].Split(',');
        playerX.Variable.Value = float.Parse(playerPositionString[0].Split('(')[1]);
        playerY.Variable.Value = float.Parse(playerPositionString[1].Split(')')[0]);
        Debug.Log("Completed Player Position" + playerX.Value + "" + playerY.Value + "moving to player Items");

        string[] playerInventoryList = loadedStrings[3].Split(':');
        for (int i = 0; i < playerInventoryList.Length; i++)
        {
            string[] tempItemAndStackNumberSplit = playerInventoryList[i].Split(',');
            
            if(1 < tempItemAndStackNumberSplit.Length)
            {
                playerItems.loadedItemNumbers[i].x = float.Parse(tempItemAndStackNumberSplit[0].Split('(')[1]);
                playerItems.loadedItemNumbers[i].y = float.Parse(tempItemAndStackNumberSplit[1].Split(')')[0]);
                //Debug.Log(tempItemAndStackNumberSplit[0].Split('(')[1] + ','  + tempItemAndStackNumberSplit[1].Split(')')[0]);
            }
        }
        playerItems.LoadingItems();
    }
}
