using UnityEngine;

public class IP_Base_RBController : MonoBehaviour
{
    #region Variables
    [Header("Base RB Properties")]
    protected Rigidbody rb;
    #endregion

    #region BuiltInMethods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb) {
            HandlePhysics();
        }
    }
    #endregion

    #region CustomMethods
    protected virtual void HandlePhysics() { }
    #endregion
}
