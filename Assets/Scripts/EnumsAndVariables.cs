﻿using System.Collections;
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
    victory
}

//Enum used to describe what animation player canvas has
public enum PlayerCanvasAnimation {
    Nothing,
    DamageTaken,
    ItemFound
};

//Enum used to describe what type of artifact this is
public enum ArtifactType {
    Poison,
    Slow
}
