using System.Collections;
using System.Collections.Generic;
using Collectables;
using UnityEngine;


namespace Collectables
{
    public class ItemCollectableBase : MonoBehaviour
    {
        [Header("Configuration")]
        public ItemType itemType;
        public string compareTag = "Player";
        public ParticleSystem particles;
        public float timeToHide = 1f;
        public GameObject graphItem;

        [Header("Sounds")]
        // public AudioSource audioSource;
        public SFXType sfxType;

        bool _collected = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void Collect()
        {
            if(_collected == true) return;
            Debug.Log("Collect");
            if (graphItem != null) graphItem.SetActive(false);
            Invoke(nameof(HideObject), timeToHide);
            _collected = true;
            OnCollect();
        }

        protected virtual void OnCollect()
        {
            Debug.Log("OnCollect");

            if (particles != null)
            {
                if (!particles.isPlaying)
                {
                    particles.Play();
                }
            }
            PlaySFX();
            // if (audioSource != null) audioSource.Play();

            ItemManager.Instance.AddByType(itemType);
        }

    }

}
