using System.Collections.Generic;
using IndiePixel;
using NUnit.Framework;
using UnityEngine;

public class IP_Rotor_Blur : MonoBehaviour, IP_IHeliRotor
{
    [Header("Rotor Blur Settings")]
    public float maxDps = 1000f;
    public List<GameObject> blades = new List<GameObject>();
    public GameObject blurGeo;
    public List<Texture2D> blurTextures;
    public Material blurMat;
    public void UpdateRotor(float dps, IP_Input_Controller input)
    {
        Debug.Log("Updating Blur : " + dps + " textures available."); 
        try {

            float normalizedDps = Mathf.InverseLerp(0f, maxDps, dps);
            int blurTextID = Mathf.FloorToInt(normalizedDps * (blurTextures.Count - 1));
            blurTextID = Mathf.Clamp(blurTextID, 0, blurTextures.Count - 1);
            if (blurMat && blurTextures.Count > 0)
            {
                blurMat.SetTexture("_BaseMap", blurTextures[blurTextID]);
            }

            if (blurTextID > 2 && blades.Count > 0)
            {
                foreach (var blade in blades)
                {
                    HandleGeoBladeViz(false);
                }

            }
            else {
                foreach (var blade in blades)
                {
                    HandleGeoBladeViz(true);
                }
            }


        } catch (System.Exception ex) {
            Debug.Log($"IP_Rotor_Blur : Error updating rotor blur: {ex.Message}");
        }


        void HandleGeoBladeViz(bool viz) {
            foreach (var blade in blades)
            {
                blade.SetActive(viz);
            }
            blurGeo.SetActive(!viz);
        }
        
       
    }
}
