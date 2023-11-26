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

        // �ѪR URL�A�ˬd�O�_�]�t JWT �Ѽ�
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