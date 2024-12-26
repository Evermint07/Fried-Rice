using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class getMoney : MonoBehaviour
{
    public InputNum inputNum;
    public InputName inputName;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetName()
    {
        string num = inputNum.inputField.text;
        string name = inputName.inputField.text;
        uint balance = gameManager.money;
        StartCoroutine(PostDataCoroutine("https://kiosk.classboard.kr/api/users", num, name, balance));
        GameManager.instance.money = 0;
    }

    IEnumerator PostDataCoroutine(string url, string num, string name, uint balance)
    {
        string jsonData = $"{{\"id\":\"{num}\",\"name\":\"{name}\",\"balance\":\"{balance}\"}}";

        // UnityWebRequest로 POST 요청 생성
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 요청 전송 및 대기
        yield return request.SendWebRequest();

        /*
        어떻게

        엄.......... ..........
        식어,,,,,,,,,,,,,,,,,,,,,,,,,,,,ㅋ
        식어.ㅋ
        식어........ㅋ
        식어........ㅋ
        식어...........ㅋ
        식어,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,ㅋ
        식어,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,ㅋ
        식어,,,,,,,,,,,,,ㅋ
        식어...........ㅋ
        식어..............ㅋ
        식어........ㅋ
        식어ㅋ
        식어,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,ㅋ

        이 사람이름이냐ㅋㅋ
        */
    }
}
