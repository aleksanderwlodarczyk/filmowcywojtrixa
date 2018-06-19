﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookFlow : MonoBehaviour {

    public string fbName;
    public string fbID;
    public bool loginAfterGet;

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

            FB.API("/me?fields=id", HttpMethod.GET, SaveID);
            FB.API("/me?fields=first_name", HttpMethod.GET, SaveName);
            Invoke("GetSurname", 0.8f);   
        }
        else
        {
            Debug.Log("error");
        }
    }

    void GetSurname()
    {
        FB.API("/me?fields=last_name", HttpMethod.GET, SaveSurname);
    }

    void SaveName(IGraphResult result)
    {
        fbName += result.ResultDictionary["first_name"].ToString();
        fbName += " ";
    }

    void SaveID(IGraphResult result)
    {
        fbID = result.ResultDictionary["id"].ToString();
        if (loginAfterGet)
        {
            GameObject.Find("Users").GetComponent<UserFlow>().LoginUser(fbID);
        }
    }

    void SaveSurname(IGraphResult result)
    {
        fbName += result.ResultDictionary["last_name"].ToString();
    }
}
