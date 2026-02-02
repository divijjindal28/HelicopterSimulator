using UnityEngine;


namespace IndiePixel {
    public class IP_BaseHeli_Input : MonoBehaviour
    {
        #region Variables
        [Header("Vase Input Properties")]
        protected float vertical = 0f;
        protected float horizontal = 0f;
        #endregion

        #region BuiltInMethods

        void Update()
        {
            HandleInputs();
        }
        #endregion

        #region CustomMethods
        protected virtual void HandleInputs()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }

        
        #endregion
    }
}

