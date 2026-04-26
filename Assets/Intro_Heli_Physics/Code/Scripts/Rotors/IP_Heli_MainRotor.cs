using UnityEngine;


namespace IndiePixel {
    public class IP_Heli_MainRotor : MonoBehaviour, IP_IHeliRotor
    {

        [Header("Main Rotor Properties")]
        public Transform lRotor;
        public Transform rRotor;
        public float maxPitch = 35f;
        
        private float currentRPMs;
        public float CurrentRPMs { get => currentRPMs; }


        public void UpdateRotor(float dps, IP_Input_Controller input)
        {
            currentRPMs = dps * 60f / 360f;
            Debug.Log($"Current RPMs: {currentRPMs}"+ "DPS : " + dps);

            transform.Rotate(Vector3.up, dps * Time.deltaTime);

            if (lRotor && rRotor) {
                lRotor.localRotation = Quaternion.Euler(-input.StickyCollectiveInput * maxPitch, 0f,0f);
                rRotor.localRotation = Quaternion.Euler(input.StickyCollectiveInput * maxPitch, 0f, 0f);
            }
            Debug.Log("Updating Main");
        }

    }

}
