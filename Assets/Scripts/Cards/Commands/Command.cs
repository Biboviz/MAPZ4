using Assets.Scripts;
using UnityEngine;

interface ICommand
{
    void Execute();
    void Undo();
}
public class CardClickCommand : ICommand
{
    private readonly Card card;

    StatsState previousStatsState;
    StatsState currentStatsState;

    public CardClickCommand(Card card)
    {
        this.card = card;
    }

    public void Execute()
    {
        var stats = card.Target.GetComponent<CharacterStats>();
        previousStatsState = stats.GetState();


        if (card.Playable())
        {
            card.Play();
            currentStatsState = stats.GetState();

        }
    }

    public void Undo()
    {
        card.Target.GetComponent<CharacterStats>().SetState(previousStatsState);

    }
}

