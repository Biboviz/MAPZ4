using System;
using System.Collections.Generic;
using UnityEngine;
public interface IFactory
{
    ICard CreateCard();
}
public interface ICard
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Play();
}
public abstract class Card : ICard
{
    private bool IsPlayable = true;
    public string Name { get; set; }
    public string Description { get; set; }
    public GameObject Target { get; set; }
    public abstract void Play();
    public void Disable()
    {
        ButtonUI?.SetInteractable(false);
    }
    public bool Playable() { return IsPlayable; }
    public CardButtonUI ButtonUI { get; set; } 

}

public interface IDamage
{
    int Damage { get; }
}
public interface IDefense
{
    int Defense { get; }
}
public interface ICost
{
    int Cost { get; }
}