using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersisableObjects : MonoBehaviour
{
    public int totalPlayers;
    public List<int> playerTypes;
    public int playerType1;
    public int playerType2;
    public int score;
    public int player1hp;
    public int player2hp;
    public static bool isCreated = false;
    public bool healthCode = false;
    public void clear()
    {
        totalPlayers = 0;
        playerTypes.Clear();
        playerType1 = 0;
        playerType2 = 0;
        player1hp = 0;
        player2hp = 0;
        score = 0;
        healthCode = false;

    }
}
