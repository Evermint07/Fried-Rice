using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    
    private uint _money;
    public uint money
    {
        get { return _money; }
        set
        {
            _money = value;
            UpdateMoneyCount();
        }
    }
    public Text moneyCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Scene 변경 후 오브젝트를 다시 찾습니다.
        player = GameObject.FindWithTag("Player");
        moneyCount = GameObject.Find("Money")?.GetComponent<Text>();

        UpdateMoneyCount();
    }

    public void AddMoney(uint value)
    {
        money += value;
    }

    public void UpdateMoneyCount()
    {
        if (moneyCount != null)
        {
            moneyCount.text = _money.ToString(); // 돈 값 갱신
        }
    }
}