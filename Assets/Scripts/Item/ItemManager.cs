using System.Collections.Generic;
using Core.Singleton;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace Collectables
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {

        public List<ItemSetup> itemSetups;

        void Start()
        {
            Reset();
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            AddByType(ItemType.COIN, (int) SaveManager.Instance.SavedValues.coins);
            AddByType(ItemType.LIFE_PACK, (int) SaveManager.Instance.SavedValues.lifePack);
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i=> i.itemType == itemType);
        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if (item.soInt.value < 0) item.soInt.value = 0;

        }

        public void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }

}
