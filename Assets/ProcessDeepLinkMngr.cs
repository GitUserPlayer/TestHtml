using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcessDeepLinkMngr : MonoBehaviour
{
    public static ProcessDeepLinkMngr Instance { get; private set; }
    public string deeplinkURL;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                // Cold start and Application.absoluteURL not null so process Deep Link.
                onDeepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
            else deeplinkURL = "[none]";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void onDeepLinkActivated(string url)
    {
        deeplinkURL = url;

        // 解析 URL，檢查是否包含 JWT 參數
        string[] urlParts = url.Split('?');
        string sceneName ="";

        if (urlParts.Length > 1)
        {
            string[] queryParams = urlParts[1].Split('&');
            sceneName = queryParams[0];

            foreach (string queryParam in queryParams)
            {
                string[] keyValue = queryParam.Split('=');
                string key = keyValue[0];
                string value = keyValue.Length > 1 ? keyValue[1] : null;

                // 根據參數名稱進行處理
                if (key.Equals("jwt"))
                {
                    // 在這裡處理 JWT 參數
                    Debug.Log("Received JWT: " + value);
                    // 在這裡添加任何您需要執行的處理邏輯
                    TokenSet.token = value;
                }
            }
        }
       

        // 根據場景名稱執行相應的操作

        bool validScene;
        switch (sceneName)
        {
            case "scene1":
                validScene = true;
                break;
            case "scene2":
                validScene = true;
                break;
            default:
                validScene = false;
                break;
        }

        // 如果是有效場景，載入該場景
        if (validScene) SceneManager.LoadScene(sceneName);
    }


    /*
    private void onDeepLinkActivated(string url)
    {
        // Update DeepLink Manager global variable, so URL can be accessed from anywhere.
        deeplinkURL = url;

        

        // Decode the URL to determine action. 
        // In this example, the application expects a link formatted like this:
        // unitydl://mylink?scene1
        string sceneName = url.Split('?')[1];




        bool validScene;
        switch (sceneName)
        {
            case "scene1":
                validScene = true;
                break;
            case "scene2":
                validScene = true;
                break;
            default:
                validScene = false;
                break;
        }
        if (validScene) SceneManager.LoadScene(sceneName);
    }*/
}