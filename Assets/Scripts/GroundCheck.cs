using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isGround = true;

    public bool GetIsGround()
    {
        return isGround;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("땅에 닿음");
            isGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
}
