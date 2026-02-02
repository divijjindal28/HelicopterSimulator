using UnityEngine;


namespace IndiePixel
{
    public enum InputType
    {
        Keyboard,
        Mobile
    }

    [RequireComponent(typeof(IP_Keyboard_Input))]
    public class IP_Input_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public InputType inputType = InputType.Keyboard;
       
        private IP_Keyboard_Input keyInput;
        #endregion

        #region Built In Methods
        private void Start()
        {
            keyInput = GetComponent<IP_Keyboard_Input>();
            if(keyInput)
                SetInputType(inputType);
        }
        #endregion

        #region Custom Methods
        void SetInputType(InputType type) {
            if (type == InputType.Keyboard)
            {
                keyInput.enabled = true;
            }
            else {
                keyInput.enabled = false;
            }
        }
        #endregion

    }
}

