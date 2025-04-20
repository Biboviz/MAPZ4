using System;
using UnityEngine;

public class CardBuilder
{
    private CustomCard card;
    private Action playAction;
    public CardBuilder ()
    {
        this.Reset();
    }
    private void Reset()
    {
        card = new CustomCard();
        playAction = null;
    }
    public void WithName(string Name)
    {
        card.Name = Name;
    }
    public void WithEffect(string effect)
    {
        card.Description += "Effect: " + effect;
    }
    public void WithTarget(GameObject target)
    {
        card.Target = target;
    }
    public void WithPlayAction(Action action)
    {
        playAction = action;
    }
    public Card Build()
    {
        card.SetPlayAction(playAction);
        Card result = card;
        Reset();
        return result;
    }
}
internal class CustomCard : Card, IDamage, IDefense, ICost
{
    public int Damage { get; private set; }
    public int Defense { get; private set; }
    public int Cost { get; private set; }

    private Action playAction;

    public override void Play()
    {
        playAction?.Invoke();
    }
    public void SetPlayAction(Action action)
    {
        playAction = action;
    }
    public void SetStats(int damage, int defense, int cost)
    {
        Damage = damage;
        Defense = defense;
        Cost = cost;
    }
}
