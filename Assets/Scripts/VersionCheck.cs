using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionCheck : MonoBehaviour
{
    public TextMeshProUGUI versionText;
    // Start is called before the first frame update
    void Start()
    {
        versionText.SetText("project-butter (Kembara Rimba) version: " + Application.version);
    }
}
