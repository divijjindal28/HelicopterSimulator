using System.Collections.Generic;
using UnityEngine;

public class CalculatingCockPitControls : MonoBehaviour
{
    


    #region Properties
    private float currentMSL;
    public float CurrentMSL
    {
        get { return currentMSL; }
    }

    private float currentAGL;
    public float CurrentAGL
    {
        get { return currentAGL; }
    }
    #endregion

    #region Constants
    const float metersToFeet = 3.28084f;
    #endregion

    #region Builtin Methods
    private void Update()
    {
        HandleAltitude();
    }
    #endregion

    #region Custom Methods
   

   

   


    void HandleAltitude()
    {
        currentMSL = transform.position.y * metersToFeet;
        Debug.Log("IP_Airplane_Altimeter NUM -2 : " + currentMSL);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.tag == "ground")
            {
                currentAGL = (transform.position.y - hit.point.y) * metersToFeet;
            }
        }
    }

    
    #endregion

}
