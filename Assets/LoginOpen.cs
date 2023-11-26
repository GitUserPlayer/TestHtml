using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginOpen : MonoBehaviour
{
    public UnityEngine.UI.Button loginButton;
    public string URL;
    public string TURL;

    public string[] urlParts;

    public string[] queryParams;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(() => 
        {
            OpenUrl(URL);
        });

       // onDeepLinkActivated(TURL);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenUrl(string URL) 
    {
        Application.OpenURL(URL);
    }

    /// <summary>
    /// 測試用
    /// </summary>
    /// <param name="url"></param>
    private void onDeepLinkActivated(string url)
    {
      

        // 解析 URL，檢查是否包含 JWT 參數
         urlParts = url.Split('?');
        
        string sceneName = urlParts[0];
        Debug.Log(urlParts[0]);

        if (urlParts.Length > 1)
        {
             queryParams = urlParts[1].Split('&');
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
        Debug.Log("sceneName : " + sceneName);


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
}

