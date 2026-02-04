using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{

    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        _saveSetup = new SaveSetup();
    }

    private void Save()
    {

        string toJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(toJson);
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();

        Save();
    }
    public void SaveItems()
    {
        _saveSetup.coins = Collectables.ItemManager.Instance.GetItemByType(Collectables.ItemType.COIN).soInt.value;
        _saveSetup.lifePack = Collectables.ItemManager.Instance.GetItemByType(Collectables.ItemType.LIFE_PACK).soInt.value;
    }

    public void SavePlayerStatus()
    {
        _saveSetup.playerHealth = Player.Instance.healthBase.CurrentLife;
    }

    private void SaveFile(string json)
    {
        string path = Application.dataPath + "/save.txt";

        File.WriteAllText(path, json);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public float coins;
    public float lifePack;
    public float playerHealth;
}
