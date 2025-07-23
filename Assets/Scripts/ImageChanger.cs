using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 전환에 필요

public class ImageChanger : MonoBehaviour
{
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] Image curImg;

    int curImgCount = 0;

    public void OnClickNextImage()
    {
        curImgCount++;

        if (curImgCount < images.Count)
        {
            curImg.sprite = images[curImgCount];
        }
        else
        {
            // 마지막 이미지에서 한 번 더 클릭 시 GameScene으로 이동
            SceneManager.LoadScene("GameScene");
        }
    }
}
