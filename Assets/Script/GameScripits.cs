using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScripits : MonoBehaviour
{
    public string userName = "";
    public string passWord = "";

    void Start()
    {
        string username = "";
        string password = "";

        string[] args = System.Environment.GetCommandLineArgs();

        foreach (string arg in args)
        {
            if (arg.StartsWith("--username"))
            {
                username = arg.Split('=')[1];
            }
            else if (arg.StartsWith("--password"))
            {
                password = arg.Split('=')[1];
            }
        }

        if (username != "" && password != "")
        {
            userName = username;
            passWord = password;
            // Use the username and password to log in

        }
        else
        {
            // Print an error message
            Debug.LogError("Username and password not provided!");
        }
    }
}
