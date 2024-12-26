using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Image 클래스를 사용하기 위해 추가

public class Win : MonoBehaviour
{
    public Player player;
    public Image targetImage; // 변경할 UI 이미지
    public Sprite newSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        Debug.Log(player.playerHealth);
        //Debug.Log(player.win);
        if (player != null && targetImage != null && newSprite != null && player.win)
        {
            targetImage.sprite = newSprite; // 스프라이트 변경 완료
            //Debug.Log("UI 이미지 스프라이트가 변경되었습니다!");
        }
    }
}