using UnityEngine;
using UnityEngine.UI;

public class SpawnWeight : MonoBehaviour
{
    public InputField inputField;
    public GameObject weightPrefab;
    protected int weightInputLimit = 12;
    protected float initialWeightSize = 0.7F;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            string input = inputField.text;

            if (input != "") {
                int weightInput = int.Parse(input);

                if (weightInput > weightInputLimit) {
                    Debug.Log("Can't spawn weight greater than 12 units.");
                    return;
                }

                GenerateWeightObject(weightInput);
            } 
        }
    }

    void GenerateWeightObject(int weight) {
        float weightScale = ((weight - 1) * 0.1F) + initialWeightSize;
        GameObject weightObject = Instantiate(weightPrefab, new Vector3(0, 3, 0), Quaternion.identity);
        weightObject.transform.localScale = new Vector3(weightScale, weightScale, weightScale);

        // Assign tag
        GameObject weightTag = weightObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        weightTag.GetComponent<Text>().text = weight.ToString();

        // Assign mass
        Rigidbody rigidbody = weightObject.GetComponent<Rigidbody>();
        rigidbody.mass = weight;
    }
}
