using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel
{
    public class AltMeter : MonoBehaviour
    {
        #region Variables
        [Header("Altemeter Properties")]
        public CalculatingCockPitControls airplane;
        public RectTransform hundredsPointer;
        public RectTransform thousandsPointer;
        #endregion

        #region Builtin Methods

        // Start is called before the first frame update
        void Update()
        {
            HandleAirplaneUI();
        }

        #endregion

        #region Interface Methods
        public void HandleAirplaneUI()
        {
            if (airplane)
            {
                float currentAlt = airplane.CurrentMSL;
                currentAlt *= 5;
                Debug.Log("IP_Airplane_Altimeter NUM -1 : " + currentAlt);
                float currentThousands = currentAlt / 1000f;
                Debug.Log("IP_Airplane_Altimeter NUM 0 : " + currentThousands);
                currentThousands = Mathf.Clamp(currentThousands, 0f, 10f);
                Debug.Log("IP_Airplane_Altimeter NUM 1 : " + currentThousands);
                float currentHundereds = currentAlt - (Mathf.Floor(currentThousands) * 1000);
                Debug.Log("IP_Airplane_Altimeter NUM 2 : " + currentHundereds);
                currentHundereds = Mathf.Clamp(currentHundereds, 0f, 1000f);
                Debug.Log("IP_Airplane_Altimeter NUM 3 : " + currentHundereds);
                Debug.Log("IP_Airplane_Altimeter currentHundreds : " + currentHundereds);
                if (thousandsPointer)
                {
                    float normalizedThousands = Mathf.InverseLerp(0f, 10f, currentThousands);
                    float thousandsRotation = 360 * normalizedThousands;
                    thousandsPointer.localRotation = Quaternion.Euler(0, 0, -thousandsRotation);

                }

                if (hundredsPointer)
                {
                    float normalizedHundereds = Mathf.InverseLerp(0f, 1000f, currentHundereds);
                    float hunderedsRotation = 360 * normalizedHundereds;
                    hundredsPointer.localRotation = Quaternion.Euler(0, 0, -hunderedsRotation);

                }
            }
        }
        #endregion
    }

}
