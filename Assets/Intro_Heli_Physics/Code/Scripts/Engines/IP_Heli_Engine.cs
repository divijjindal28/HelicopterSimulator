using UnityEngine;

namespace IndiePixel{
    public class IP_Heli_Engine : MonoBehaviour
    {

        #region Variables
        public float maxHP = 140f;
        public float maxRPM = 2700f;
        public float powerDelay = 2f;
        public AnimationCurve powerCurve = new AnimationCurve(new Keyframe(0f,0f),new Keyframe(1f,1f));

        #endregion

        #region Properties
        private float currentHP;
        public float CurrentHP {
            get { return currentHP; }
        }

        private float currentRPM;
        public float CurrentRPM {
            get { return currentRPM; }
        }

        #endregion

        #region Built In Methods
        void Start()
        {

        }
        #endregion

        #region Custiom Methods
        public void UpdateEngine(float throttleInput) {
            Debug.Log("Updating Engine with Throttle: " + throttleInput);
            //Calculate Horse Power
            float wantedHP = powerCurve.Evaluate(throttleInput) * maxHP;
            currentHP = Mathf.Lerp(currentHP, wantedHP, Time.deltaTime * powerDelay);

            //calculate RPMs
            float wantedRPM = throttleInput * maxRPM;
            currentRPM = Mathf.Lerp(currentRPM, wantedRPM, Time.deltaTime * powerDelay);

        }
        #endregion

    }
}

