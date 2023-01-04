using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelaunchScript : MonoBehaviour
{
    public Button relaunchButton;
    public GameScripits userDetails = new GameScripits();
    private string userName;
    private string passWord;

    void Start()
    {
        userName = userDetails.userName; 
        passWord = userDetails.passWord;
        relaunchButton.onClick.AddListener(LaunchExecutable);
    }

    void LaunchExecutable()
    {
        string username = userName;
        string password = passWord;

        //// Build the command-line arguments string
        //string arguments = $"--username {username} --password {password}";

        //// Launch the executable file with the command-line arguments
        //System.Diagnostics.Process.Start("C:\path\to\executable.exe", arguments);
    }
}
