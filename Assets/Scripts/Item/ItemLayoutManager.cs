using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;


namespace Collectables
{
    public class ItemLayoutManager : Singleton<ItemLayoutManager>
    {
        public ItemLayout prefabLayout;
        public Transform container;

        public List<ItemLayout> itemLayouts;

        void Start()
        {
            CreateItems();
        }

        private void CreateItems()
        {
            foreach (var setup in ItemManager.Instance.itemSetups)
            {
                var item = Instantiate(prefabLayout, container);

                if (setup.itemType == ItemType.LIFE_PACK)
                {
                    item.transform.Find("UiButton").gameObject.SetActive(true);
                }

                item.Load(setup);
                itemLayouts.Add(item);
            }
        }

    }

}
