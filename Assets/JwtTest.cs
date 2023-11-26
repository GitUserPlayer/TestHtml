using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TokenSet
{
    public static string token = "";
}


public class JwtTest : MonoBehaviour
{
    
    public TMPro.TextMeshProUGUI textMeshPro;

    bool input;
    
    // Start is called before the first frame update
    void Start()
    {
        input = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (input == false && TokenSet.token != null ) 
        {
            textMeshPro.text = TokenSet.token;
            input = true;
        }
    }
}
