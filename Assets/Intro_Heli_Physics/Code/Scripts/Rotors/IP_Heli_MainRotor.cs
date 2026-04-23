using UnityEngine;


namespace IndiePixel {
    public class IP_Heli_MainRotor : MonoBehaviour, IP_IHeliRotor
    {

        [Header("Main Rotor Properties")]
        public Transform lRotor;
        public Transform rRotor;
        public float maxPitch = 35f;
        void Start()
        {

        }


        public void UpdateRotor(float dps, IP_Input_Controller input)
        {
            transform.Rotate(Vector3.up, dps);

            if (lRotor && rRotor) {
                lRotor.localRotation = Quaternion.Euler(input.CollectiveInput * maxPitch, 0f,0f);
                rRotor.localRotation = Quaternion.Euler(-input.CollectiveInput * maxPitch, 0f, 0f);
            }
            Debug.Log("Updating Main");
        }

    }

}
