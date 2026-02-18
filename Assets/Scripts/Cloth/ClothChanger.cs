using UnityEngine;

namespace Cloth
{

    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer skinnedMeshRenderer;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        void Awake()
        {
            _defaultTexture = (Texture2D) skinnedMeshRenderer.materials[0].GetTexture(shaderIdName);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            skinnedMeshRenderer.materials[0].SetTexture(shaderIdName, setup.texture);

        }

        public void ResetTexture()
        {
            skinnedMeshRenderer.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }

    }
}