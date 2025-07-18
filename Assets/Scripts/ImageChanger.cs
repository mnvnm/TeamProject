using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] Image curImg;
    int curImgCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickNextImage()
    {
        curImgCount++;
        if (curImgCount < images.Count)
        {
            curImg.sprite = images[curImgCount];
        }
    }
}
