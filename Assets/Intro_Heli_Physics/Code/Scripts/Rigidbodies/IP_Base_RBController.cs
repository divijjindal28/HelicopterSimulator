using UnityEngine;

namespace IndiePixel {
    [RequireComponent(typeof(Rigidbody))]
    public class IP_Base_RBController : MonoBehaviour
    {
        #region Variables
        [Header("Base Properties")]
        public float weightInLbs = 10f;
        public Transform cog;
        const float LbsToKg = 0.45359237f;
        const float KgToLbs = 2.20462f;

        protected Rigidbody rb;
        protected float weight;
        #endregion

        #region BuiltInMethods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            float finalKg = weightInLbs * LbsToKg;
            weight = finalKg;
            rb = GetComponent<Rigidbody>();
            if (rb) {
                rb.mass = weight;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (rb)
            {
                HandlePhysics();
            }
        }
        #endregion

        #region CustomMethods
        protected virtual void HandlePhysics() { }
        #endregion
    }
}

