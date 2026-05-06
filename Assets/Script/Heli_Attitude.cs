using System.Collections;
using System.Collections.Generic;
using IndiePixel;
using UnityEngine;

namespace IndiePixel {
    public class Heli_Attitude : MonoBehaviour
    {
        #region Variables
        [Header("Attitude Indicator Properties")]
        public IP_Heli_Controller airplane;
        public RectTransform bgRect;
        public RectTransform arrowRect;
        
        #endregion

        public const float mphtoKnots = 0.868976f;

        #region Interface Methods
        public void Update()
        {
            Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM : WORKING");
            if (airplane && arrowRect)
            {
                float bankAngle = Vector3.Dot(airplane.transform.right, Vector3.up) * Mathf.Rad2Deg;
                float pitchAngle = Vector3.Dot(airplane.transform.forward, Vector3.up) * Mathf.Rad2Deg;


                if (bgRect) {
                    Quaternion bankRot = Quaternion.Euler(0, 0, bankAngle);
                    bgRect.transform.localRotation = bankRot;

                    Vector3 wantedPosition = new Vector3(0f, -pitchAngle/1000 , 0f);
                    bgRect.anchoredPosition = wantedPosition;
                    if (arrowRect) {
                        arrowRect.transform.localRotation = bankRot;
                    }
                }

            }
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

