using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Collectables
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uiIcon;
        public TextMeshProUGUI uiTmp;
        
        private ItemSetup _currSetup;


        public void Load(ItemSetup setup)
        {
            _currSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uiIcon.sprite = _currSetup.icon;
        }

        void Update()
        {
            uiTmp.text = _currSetup.soInt.value.ToString();
        }
    }
}
