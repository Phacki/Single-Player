using UnityEngine;
using TMPro;

public class WinCounter : MonoBehaviour
{
    private TextMeshProUGUI win;

    public void OnDisable()
    {
        win.text = "You Win";
    }
}
