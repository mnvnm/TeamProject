using UnityEngine;

public class MapRotator : MonoBehaviour
{
    float rotateSpeed = 30f;
    float targetZ = 0f;
    float lerpSpeed = 7f; // 부드러움 정도
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetZ = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Inst.IsGameOver) return;
        if (GameManager.Inst.game.player.GetIsInteractive()) return;

        if(!GameManager.Inst.game.player.IsStun)
        {
            // 입력에 따라 목표 각도 변경
            if (Input.GetKey(KeyCode.Q))
            {
                targetZ += rotateSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                targetZ -= rotateSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                rotateSpeed = 90f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                rotateSpeed = 30f;
            }
        }

        // 현재 각도에서 목표 각도로 부드럽게 회전
        Quaternion targetRot = Quaternion.Euler(0, 0, targetZ);

        if (GameManager.Inst.game.player.IsStun)
        {
            targetRot = transform.rotation;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * lerpSpeed);
    }
}
