using UnityEngine;

public class MapRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0,0,30 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0,0,-30 * Time.deltaTime));
        }
    }
}
