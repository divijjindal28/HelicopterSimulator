using UnityEngine;


namespace IndiePixel
{
    public class IP_Heli_TailRotor : MonoBehaviour, IP_IHeliRotor
    {

        [Header("Tail Rotor Settings")]
        public float rotationSpeedModifier = 1.5f;

        [Header("Main Rotor Properties")]
        public Transform lRotor;
        public Transform rRotor;
        public float maxPitch = 45f;
        void Start()
        {

        }

        public void UpdateRotor(float dps, IP_Input_Controller input)
        {
            transform.Rotate(Vector3.right, dps * rotationSpeedModifier);
            if (lRotor && rRotor)
            {
                lRotor.localRotation = Quaternion.Euler(0f, input.PedalInput * maxPitch, 0f);
                rRotor.localRotation = Quaternion.Euler(0f, -input.PedalInput * maxPitch, 0f);
            }
            Debug.Log("Updating Tail");
        }
    }

}
