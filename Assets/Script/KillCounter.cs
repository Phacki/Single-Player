using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public TextMeshProUGUI Tags;
    public PlayerMovement kill;

    public void OnDisable()
    {
        Debug.Log("KillCounted");
        kill.kills++;
        Tags.text = kill.kills.ToString();
    }
}
