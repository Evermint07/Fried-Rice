using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Click : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartCurrentScene()
    {
        // 현재 Scene의 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 현재 Scene 다시 로드
        SceneManager.LoadScene(currentSceneName);
    }
}
