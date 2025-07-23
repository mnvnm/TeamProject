using UnityEngine;

public class MapRotator : MonoBehaviour
{
    float RotateSpeed = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Inst.game.player.GetIsInteractive()) return;
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0, -RotateSpeed * Time.deltaTime));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            RotateSpeed = 90;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RotateSpeed = 30;
        }
    }
}
