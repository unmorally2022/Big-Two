using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CardStruct
{
    public int id;
    public int Suit;
    public int Rank;
}

public enum GameState
{
    Pause,    
    SelectingChar,
    SufflingCard,
    SharingCard,
    ArrangingCard,
    CheckingRow,    
    CheckingFinal
}

public enum PlayerActionState{
    None, Dragging, MovingCard
}

public enum PlayerState {
    Iddle, ArrangingCard, StopingArranging
}

public enum MonsterState
{
    iddle, win, loose
}

public enum MonsterType
{
    orc, dragon, mummy, ghost
}