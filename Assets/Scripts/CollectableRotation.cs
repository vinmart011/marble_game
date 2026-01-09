using UnityEngine;

public class CollectableRotation : MonoBehaviour
{

    public Vector3 rotation = new Vector3(30,40,45);

    // Update is called once per frame
    void Update()
    {
        
            transform.Rotate(rotation * Time.deltaTime);
        
    }
}
