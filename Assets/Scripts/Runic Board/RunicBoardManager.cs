using UnityEngine;
using System.Collections;

public class RunicBoardManager{

    static private RunicBoardManager _instance;

    private RunicBoard _boardPlayer1;
    //private RunicBoard _boardPlayer2;

    RunicBoardManager() { }

    public static RunicBoardManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new RunicBoardManager();
        }
        return _instance;
    }

    public void RegisterBoard(RunicBoard board)
    {
        _boardPlayer1 = board;
    }

    public RunicBoard GetBoardPlayer1()
    {
        return _boardPlayer1;
    }

    /*public RunicBoard GetBoardPlayer2()
    {
        return _boardPlayer2;
    }*/
}
