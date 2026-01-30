using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{

    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string tagToCompare = "Player";

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(tagToCompare))
            {
                Collect();
            }
        }
        protected virtual void Collect()
        {
            HideObjetct();
        }

        private void HideObjetct()
        {
            gameObject.SetActive(false);
        }


    }
}