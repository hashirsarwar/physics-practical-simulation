using UnityEngine;

public class MeterRodController : MonoBehaviour
{
    private Rigidbody rigidBody;

    // Start is called before the first frame update.
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void ToggleMeterRodRotation(bool freeze) {
        if (freeze) {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            transform.rotation = new Quaternion();
        } else {
            rigidBody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
