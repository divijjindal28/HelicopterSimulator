using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;
using System.Data.Common;

namespace IndiePixel {
    public class IP_Heli_Rotor_Controller : MonoBehaviour
    {
        #region Variables
        private List<IP_IHeliRotor> rotors;
        #endregion

        void Start()
        {
            rotors =GetComponentsInChildren<IP_IHeliRotor>().ToList<IP_IHeliRotor>();
        }


        #region CustomMethods
        public void UpdateRotors(IP_Input_Controller input, float currentRPM) {

            //Degrees per second calculation
            float dps = ((currentRPM * 360f)/60) * Time.deltaTime;

            //update our rotors
            Debug.Log("Updating Rotor Controller : "+ rotors.Count);
            if (rotors.Count > 0) {
                foreach (var rotor in rotors) {
                    rotor.UpdateRotor(dps,input);
                }


            }
           
        }
        #endregion

        

    }

}
