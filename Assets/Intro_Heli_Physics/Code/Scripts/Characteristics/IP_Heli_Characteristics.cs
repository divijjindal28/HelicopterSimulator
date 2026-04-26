using UnityEngine;

namespace IndiePixel
{
    public class IP_Heli_Characteristics : MonoBehaviour
    {
        [Header("Lift Properties")]
        public float maxLiftForce = 10f;
        public IP_Heli_MainRotor mainRotor;

        [Header("Tail Rotor Properties")]
        public float tailForce = 2f;

        [Header("Cyclic Properties")]
        public float cyclicPowerForce = 2f;
        public float cyclicForceMultiplier = 1000f;

        [Header("AutoLevel Properties")]
        public float autoLevelForce = 2f;

        private Vector3 flatFow;
        private float ForwardDot;
        private Vector3 flatRight;
        private float RightDot;



        public void UpdateCharacteristics(Rigidbody rb, IP_Input_Controller input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
            CalculateAngles();
            AutoLevel(rb);
        }

        protected virtual void HandleLift(Rigidbody rb, IP_Input_Controller input) {

            //Debug.Log("Handling Lift");
            if (mainRotor) {
                Vector3 liftForce = transform.up * (Physics.gravity.magnitude + maxLiftForce) * rb.mass;
                //Debug.Log("IP_Heli_Characteristics LiftForce : "+ liftForce);
                float normalzedRPM = mainRotor.CurrentRPMs / 500;
                //Debug.Log("IP_Heli_Characteristics normalizedRPM : " + normalzedRPM);
                rb.AddForce(liftForce * Mathf.Pow(normalzedRPM, 2f) * Mathf.Pow(input.StickyCollectiveInput, 2f), ForceMode.Force);
                //rb.AddForce(liftForce);
                Debug.Log("IP_Heli_Characteristics : " + transform.up * (Physics.gravity.magnitude) * rb.mass + "  " + liftForce + "  " + normalzedRPM + "    " + input.StickyCollectiveInput + "    " + liftForce * Mathf.Pow(normalzedRPM, 2f) * Mathf.Pow(input.StickyCollectiveInput, 2f));

            }

        }

        public virtual void HandleCyclic(Rigidbody rb, IP_Input_Controller input) {
            //Debug.Log("Handling Cyclic");
            float cyclicZForce = input.CyclicInput.x * cyclicPowerForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);

            float cyclicXForce = -input.CyclicInput.y * cyclicPowerForce;
            rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);

            Vector3 forwardVec = flatFow * ForwardDot;
            Vector3 rightVec = flatRight * RightDot;
            Vector3 FinalCyclicVector = Vector3.ClampMagnitude(forwardVec + rightVec, 1f)* cyclicPowerForce * cyclicForceMultiplier;
            rb.AddForce(FinalCyclicVector, ForceMode.Force);

        }

        public virtual void HandlePedals(Rigidbody rb, IP_Input_Controller input) {
            rb.AddTorque(Vector3.up * tailForce * input.PedalInput, ForceMode.Acceleration);
        }

        private void CalculateAngles() {
            //To be implemented in derived classes
            flatFow = transform.forward;
            flatFow.y = 0f;
            flatFow = flatFow.normalized;
            flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;

            ForwardDot = Vector3.Dot(transform.up, flatFow);
            RightDot = Vector3.Dot(transform.up, flatRight);
        }

        private void AutoLevel(Rigidbody rb) {
            //To be implemented in derived classes

            float rightForce = -ForwardDot * autoLevelForce;
            float forwardForce = RightDot * autoLevelForce;
            rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
        }
    }
}
