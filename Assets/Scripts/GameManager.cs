using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MENU, CINEMATIC, GAME, TEST }
public enum Cinematic { INTRO, END1, END2 }

public static class GameManager
{
    public static Cinematic cinematicChosen;
    private static GameState gameState;

    public static void SetGameState(GameState state, Cinematic cinematic = Cinematic.INTRO)
    {
        gameState = state;
        cinematicChosen = cinematic;
        SceneManager.LoadScene((int)gameState);
    }
}