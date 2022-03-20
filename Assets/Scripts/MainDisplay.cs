using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDisplay : MonoBehaviour
{
    public Text respawnText;
    public GameObject respawnPanel;

    public void ShowRespawn(float respawnTime)
    {
        respawnPanel.gameObject.SetActive(true);
        respawnText.text = string.Format("Respawning in ... {0}", Mathf.Ceil(respawnTime));
    }
    public void HideRespawn()
    {
        respawnPanel.gameObject.SetActive(false);
    }
}
