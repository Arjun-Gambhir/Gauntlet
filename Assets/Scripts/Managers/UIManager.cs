using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Create templates for the players in the inspector
[System.Serializable]
public class PlayerUI
{
    public Players playerRef;
    public Text scoreText;
    public Text healthText;
    public Text potionsText;
    public Text keysText;
}

public class UIManager : MonoBehaviour
{
    public List<PlayerUI> allPlayers = new List<PlayerUI>();
    public static UIManager instance;
    public Text levelText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int playerNumber = 0; playerNumber < allPlayers.Count; playerNumber++)
        {
            UpdateUIText(playerNumber);
        }
    }

    // each player can then tell this manager what needs to be updated. The manager will find the information based on the reference found in the template
    public void UpdateUIText(int playerIndex)
    {
        allPlayers[playerIndex].scoreText.text = allPlayers[playerIndex].playerRef.score.ToString();
        allPlayers[playerIndex].healthText.text = allPlayers[playerIndex].playerRef.health.ToString();
        allPlayers[playerIndex].potionsText.text = "Potions: " + allPlayers[playerIndex].playerRef.potionCount.ToString();
        allPlayers[playerIndex].keysText.text = "Keys: " + allPlayers[playerIndex].playerRef.keyCount.ToString();
    }

}
