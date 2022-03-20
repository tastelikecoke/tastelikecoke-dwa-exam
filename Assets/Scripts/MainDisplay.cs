using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDisplay : MonoBehaviour
{
    [Header("Required References")]
    public Text respawnText;
    public GameObject respawnPanel;
    public Text killsText;
    public Text highscoreKillsText;
    public Image hpImage;

    public void UpdateKills(int currentKills, int highscoreKills)
    {
        killsText.text = string.Format("kills: {0}", currentKills);
        highscoreKillsText.text = string.Format("highscore: {0}", highscoreKills);
    }
    public void UpdateHealth(float hpRatio)
    {
        hpImage.fillAmount = hpRatio;
    }

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
