using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel
{
    public class IP_Airplane_Tachometer : MonoBehaviour
    {
        #region Variables
        [Header("Tachometer Properties")]
        public IP_Heli_Engine engine;
        public RectTransform pointer;
        public float maxRPM = 2700;
        public float maxRotation = 312;

        private float finalRotation;
        public float pointerSpeed = 2f;
        #endregion


        #region Interface Methods
        

        public void Update()
        {
            Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM : WORKING");
            if (engine && pointer)
            {
                float normalizedRPM = Mathf.InverseLerp(0f, maxRPM, engine.CurrentRPM);
                Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM : " + normalizedRPM);
                float wantedRotation = maxRotation * -normalizedRPM;
                finalRotation = Mathf.Lerp(finalRotation, wantedRotation, Time.deltaTime * pointerSpeed);
                pointer.localRotation = Quaternion.Euler(0, 0, wantedRotation);
            }
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

