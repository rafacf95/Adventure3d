using UnityEngine;

namespace Cloth
{

    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public float duration = 5f;
        public string tagToCompare = "Player";

        [Header("Sounds")]
        public SFXType sfxType;

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(tagToCompare))
            {
                var setup = ClothManager.Instance.GetSetupByType(clothType);
                Player.Instance.ChangeTexture(setup, duration);
                PlaySFX();
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