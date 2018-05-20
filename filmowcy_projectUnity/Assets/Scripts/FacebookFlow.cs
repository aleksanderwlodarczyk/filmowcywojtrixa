using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookFlow : MonoBehaviour {

    public string fbName;

    private bool APICalled;

    void Start () {
        fbName = "";
        APICalled = false;
        FB.Init();
    }

    private void Update()
    {
        if(!APICalled && FB.IsInitialized)
        {
            FacebookConnect();
            APICalled = true;
        }
    }

    void FacebookConnect()
    {
        var perms = new List<string>() { "public_profile" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    void AuthCallback(IResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = AccessToken.CurrentAccessToken;

            FB.API("/me?fields=first_name", HttpMethod.GET, SaveName);
            FB.API("/me?fields=last_name", HttpMethod.GET, SaveSurname);
        }
        else
        {
            Debug.Log("error");
        }
    }

    void SaveName(IGraphResult result)
    {
        fbName += result.ResultDictionary["first_name"].ToString();
        fbName += " ";
    }

    void SaveSurname(IGraphResult result)
    {
        fbName += result.ResultDictionary["last_name"].ToString();
    }
}
