using UnityEngine;
using UnityEngine.UI;

public class DragObject : MonoBehaviour
{
    // This keeps record of the distance of the weight relative
    // to the meter rod. 
    private GameObject distanceTag;
    // The plane the object is currently being dragged on.
    private Plane dragPlane;
    // The difference between where the mouse is on the drag plane and 
    // where the origin of the object is on the drag plane.
    private Vector3 offset;
    private Camera myMainCamera;

    void Start() {
        myMainCamera = Camera.main; // Camera.main is expensive; cache it here.
        distanceTag = GameObject.FindGameObjectWithTag("DistanceTag");
    }

    void OnMouseDown() {
        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    void OnMouseDrag() {
        float planeDist;
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        dragPlane.Raycast(camRay, out planeDist);
        Vector3 mPos = camRay.GetPoint(planeDist) + offset;

        // Freeze position of weight in the x axis for some time if
        // a whole number distance is reached.
        if (mPos.x != 0) {
            if (Mathf.Abs(mPos.x % 1) < 0.3) {
                mPos.x = Mathf.Round(mPos.x);
            }
        }

        transform.position = mPos;
        // Update distance on the meter rod.
        distanceTag.GetComponent<Text>().text = "Distance: " + transform.position.x;
    }
}
