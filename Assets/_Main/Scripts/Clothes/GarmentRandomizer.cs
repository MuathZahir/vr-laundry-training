using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GarmentRandomizer : MonoBehaviour
{
    [SerializeField] private Shader shader;
    [SerializeField] private Material templateMat;
    [SerializeField] private MeshRenderer heldClothRenderer;
    [SerializeField] private SkinnedMeshRenderer idleClothRenderer;
    [SerializeField] private GarmentMaterialTextures[] garmentMaterialTextures;
    [SerializeField] private Texture2D softStainMask;
    [SerializeField] private Texture2D hardStainMask;
    [SerializeField] private StainColor[] stainColors;

    private Material garmentMat;

    public void RandomizeMaterials(GarmentInfo info)
    {
        garmentMat = new Material(templateMat);
        
        Randomize(info);

        heldClothRenderer.material = garmentMat;
        idleClothRenderer.material = garmentMat;
    }

    private void Randomize(GarmentInfo info)
    {

        System.Random random = new System.Random();
        
        // Set the garment material textures in the garmentMat
        var randomIndex = random.Next(0, garmentMaterialTextures.Length);
        var texture = garmentMaterialTextures[randomIndex];
        
        garmentMat.SetTexture("_T_M_1_Cloth_AlbedoTransparency", texture.Albedo);
        garmentMat.SetTexture("_T_M_1_Cloth_MetallicSmoothness", texture.Metallic);
        garmentMat.SetTexture("T_M_Cloth_1_Normal", texture.Normal);
        
        // garmentMat.SetTexture("_Cloth_Albedo", texture.Albedo);
        // garmentMat.SetTexture("_Cloth_Metallic", texture.Metallic);
        // garmentMat.SetTexture("_Cloth_Normal", texture.Normal);

        info.LaundryColor = texture.LaundryColor;

        if (stainColors.Length == 0)
        {
            info.StainType = StainType.Any;
            garmentMat.SetFloat("_StainsOpacity", 0);
            garmentMat.SetFloat("_DirtOpacity", 0);
            
            // garmentMat.SetFloat("_Stain_Opacity", 0);
            // garmentMat.SetFloat("_Dirt_Opacity", 0);
            
            return;
        }
        
        // Randomize stain color
        var stainColor = stainColors[random.Next(0, stainColors.Length)];
        
        info.StainType = stainColor.StainType;
        
        garmentMat.SetColor("_StainColor", stainColor.Color);
        // garmentMat.SetColor("_Stain_Color", stainColor.Color);
        
        // Randomize stain mask
        var stainMask = stainColor.StainType == StainType.Hard ? hardStainMask : softStainMask;
        
        garmentMat.SetTexture("_Mask_1", stainMask);
        
        // Randomize stain and dirt amount
        var stainAmount = stainColor.StainType == StainType.Hard ? random.NextDouble() * 0.2 + 0.8 
            : random.NextDouble() * 0.2 + 0.3;
        
        var dirtAmount = random.NextDouble() * 0.5 + 0.5;
        
        garmentMat.SetFloat("_StainsOpacity", (float)stainAmount);
        garmentMat.SetFloat("_DirtOpacity", (float)dirtAmount);
        
        // garmentMat.SetFloat("_Stain_Opacity", stainAmount);
        // garmentMat.SetFloat("_Dirt_Opacity", dirtAmount);
        
        // Randomize stain and dirt texture tiling and offset
        var stainSize = (float)(random.NextDouble() * 2.5 + 0.5);
        var dirtSize = (float)(random.NextDouble() * 2.5 + 0.5);
        
        var stainOffset = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
        var dirtOffset = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
        
        garmentMat.SetTextureScale("_Mask_1", new Vector2(stainSize, stainSize));
        garmentMat.SetTextureScale("_Mask_2", new Vector2(dirtSize, dirtSize));
        
        garmentMat.SetTextureOffset("_Mask_1", stainOffset);
        garmentMat.SetTextureOffset("_Mask_2", dirtOffset);
        
        // garmentMat.SetTextureScale("_Stain_Mask", new Vector2(stainSize, stainSize));
        // garmentMat.SetTextureScale("_Dirt_Mask", new Vector2(dirtSize, dirtSize));
        //
        // garmentMat.SetTextureOffset("_Stain_Mask", stainOffset);
        // garmentMat.SetTextureOffset("_Dirt_Mask", dirtOffset);
    }

    public void CleanGarment()
    {
        garmentMat.SetFloat("_StainsOpacity", 0f);
        garmentMat.SetFloat("_DirtOpacity", 0f);
        // garmentMat.SetFloat("_Stain_Opacity", 0f);
        // garmentMat.SetFloat("_Dirt_Opacity", 0f);
    }

    public void DirtyGarment()
    {
        garmentMat.SetFloat("_StainsOpacity", 1f);
        garmentMat.SetFloat("_DirtOpacity", 1f);
    }

    [Serializable]
    public struct GarmentMaterialTextures
    {
        public Texture2D Albedo;
        public Texture2D Metallic;
        public Texture2D Normal;
        
        public LaundryColor LaundryColor;
    }

    [Serializable]
    public struct StainColor
    {
        public string Name;
        public Color Color;
        public StainType StainType;
    }
}
