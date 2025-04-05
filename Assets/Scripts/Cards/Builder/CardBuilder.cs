using System;
using UnityEngine;

public class CardBuilder
{
    private Card card;
    private Action playAction;
    public CardBuilder CreateBaseCard(string name, string desc)
    {
        card = new CustomCard(); 
        card.Name = name;
        card.Description = desc;
        return this;
    }
    public CardBuilder WithEffect(string effect)
    {
        card.Description += "\nEffect: " + effect;
        return this;
    }
    public CardBuilder WithTarget(GameObject target)
    {
        card.Target = target;
        return this;
    }
    public CardBuilder WithPlayAction(Action action)
    {
        playAction = action;
        return this;
    }
    public Card Build()
    {
        if (card is CustomCard customCard)
        {
            customCard.SetPlayAction(playAction);
        }
        Card result = card;
        card = null;
        return result;
    }
}
internal class CustomCard : Card
{
    private Action playAction;
    public override void Play()
    {
        playAction?.Invoke();
    }
    public void SetPlayAction(Action action)
    {
        playAction = action;
    }
}