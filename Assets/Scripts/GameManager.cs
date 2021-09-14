using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MENU, CINEMATIC, GAME, TEST }

public static class GameManager
{
    private static GameState gameState;

    public static void SetGameState(GameState state)
    {
        gameState = state;
        SceneManager.LoadScene((int)gameState);
    }
}