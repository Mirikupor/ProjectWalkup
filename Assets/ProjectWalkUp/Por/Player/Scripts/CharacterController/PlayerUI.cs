using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI prompText;

    public void UpdateText(string promp)
    {
        prompText.text = promp;
    }
}
