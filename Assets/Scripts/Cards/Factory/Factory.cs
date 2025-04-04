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
    public string Name { get; set; }
    public string Description { get; set; }
    public GameObject Target { get; set; }

    public abstract void Play();
}
