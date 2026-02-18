using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{

    public int lastLevel;
    public Action<SaveSetup> fileLoaded;

    [SerializeField] private SaveSetup _saveSetup;
    public SaveSetup SavedValues
    {
        get { return _saveSetup; }
    }
    private string _path;

    protected override void Awake()
    {
        base.Awake();
        _path = Application.dataPath + "/save.txt";
        DontDestroyOnLoad(gameObject);
        CreateNewSave();
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup
        {
            lastLevel = 0
        };

    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
    }

    public void SaveLastChekpoint(int ChekpointKey)
    {
        _saveSetup.lastCheckpoint = ChekpointKey;
    }

    public void SaveItems()
    {
        _saveSetup.coins = Collectables.ItemManager.Instance.GetItemByType(Collectables.ItemType.COIN).soInt.value;
        _saveSetup.lifePack = Collectables.ItemManager.Instance.GetItemByType(Collectables.ItemType.LIFE_PACK).soInt.value;
    }

    public void SavePlayerStatus()
    {
        _saveSetup.playerHealth = Player.Instance.healthBase.CurrentLife;
        _saveSetup.clothSetup = Player.Instance.CurrentClothSetup;
    }

    public void SavePlayerPosition(Vector3 position)
    {
        _saveSetup.playerPosition = position;
    }

    private void CreateFile(string json)
    {
        File.WriteAllText(_path, json);
    }

    private void SaveToFile()
    {
        string toJson = JsonUtility.ToJson(_saveSetup, true);
        CreateFile(toJson);
    }

    public void SaveGame(int level, int ChekpointKey, Vector3 playerPosition)
    {
        SaveLastLevel(level);
        SaveLastChekpoint(ChekpointKey);
        SaveItems();
        SavePlayerStatus();
        SavePlayerPosition(playerPosition);

        SaveToFile();
    }

    public void Load()
    {
        string file = "";
        if (File.Exists(_path))
        {
            file = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(file);
        }
        else
        {
            CreateNewSave();
            SaveToFile();
        }

        fileLoaded.Invoke(_saveSetup);

    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int lastCheckpoint;
    public float coins;
    public float lifePack;
    public float playerHealth;
    public Vector3 playerPosition;
    public Cloth.ClothSetup clothSetup;
}
