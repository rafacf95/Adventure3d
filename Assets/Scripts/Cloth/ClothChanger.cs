using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Texture2D texture;
    public string shaderIdName = "_EmissionMap";

    [NaughtyAttributes.Button]
    private void ChangeTexture()
    {
        skinnedMeshRenderer.materials[0].SetTexture(shaderIdName, texture);
    }
}
