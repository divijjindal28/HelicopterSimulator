using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    public class IP_Airplane_Airspeed : MonoBehaviour
    {
        #region Variables
        [Header("Airspeed Indicator Properties")]
        public IP_Heli_Characteristics characteristics;
        public RectTransform pointer;
        public float maxIndicatedKnots = 160f;
        public float maxRotation = 360f;

        private float finalRotation;
        public float pointerSpeed = 2f;
        #endregion

        public const float mphtoKnots = 0.868976f;

        #region Interface Methods


        public void Update()
        {
            Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM : WORKING");
            if (characteristics && pointer)
            {
                float currentKnots = characteristics.speedMPH * mphtoKnots;
                float normalizedKnots = Mathf.InverseLerp(0f, maxIndicatedKnots, currentKnots);
                float wantedRotation = maxRotation * normalizedKnots;
                //finalRotation = Mathf.Lerp(finalRotation, wantedRotation, Time.deltaTime * pointerSpeed);
                pointer.localRotation = Quaternion.Euler(0, 0, -wantedRotation);
            }
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

