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
    /// ���ե�
    /// </summary>
    /// <param name="url"></param>
    private void onDeepLinkActivated(string url)
    {
      

        // �ѪR URL�A�ˬd�O�_�]�t JWT �Ѽ�
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

                // �ھڰѼƦW�ٶi��B�z
                if (key.Equals("jwt"))
                {
                    // �b�o�̳B�z JWT �Ѽ�
                    Debug.Log("Received JWT: " + value);
                    // �b�o�̲K�[����z�ݭn���檺�B�z�޿�
                    TokenSet.token = value;
                }
            }
        }
        Debug.Log("sceneName : " + sceneName);


        // �ھڳ����W�ٰ���������ާ@

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

        // �p�G�O���ĳ����A���J�ӳ���
        if (validScene) SceneManager.LoadScene(sceneName);
    }
}

