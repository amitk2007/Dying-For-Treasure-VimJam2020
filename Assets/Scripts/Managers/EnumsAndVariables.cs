using System.Collections;
using System.Collections.Generic;

//Enum used to describle what animation player has
public enum PlayerAnimationState
{
    idle,
    walking,
    crouch,
    jump,
    hurt,
    death,
    victory,
    climbing
}

//Enum used to describe what animation player canvas has
public enum PlayerCanvasAnimation
{
    Nothing,
    DamageTaken,
    ItemFound
};

//Enum used to describe what type of artifact this is
public enum ArtifactType
{
    Poison,
    Slow,
    Flying,
    Null
}

//Enum used to describe kind of toggle
public enum ToggleType
{
    Music,
    Sound
}

//Add new strings here when new levels are added to game!
public class LevelList
{
    public static string[] Levels = { "Movment_Tutorial", "Poison_Tutorial", "Flying_Tutorial", "Slow_Tutorial", "Level2" };
    public static int CorrentLevel;
    public static void SetCurrentLevel(int level)
    {
        LoadSettins.SaveIntToPrefs("CorrentLevel", level);
        CorrentLevel = level;
    }
}
