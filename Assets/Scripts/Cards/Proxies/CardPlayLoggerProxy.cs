using Assets.Scripts;
using UnityEngine;

public class CardPlayLoggerProxy : Card
{
    private readonly Card _realCard;

    public CardPlayLoggerProxy(Card realCard)
    {
        _realCard = realCard;

        // Copy important fields
        this.Name = realCard.Name;
        this.Description = realCard.Description;
        this.Target = realCard.Target;
    }

    public override void Play()
    {
        if (GameManager.Instance != null && GameManager.Instance.DevMode)
        {
            Debug.Log($"[LOG] Playing card: {_realCard.Name} targeting {_realCard.Target?.name}");
        }

        _realCard.Play();

        if (GameManager.Instance != null && GameManager.Instance.DevMode)
        {
            Debug.Log($"[LOG] Card played: {_realCard.Name}");
            Debug.Log($"[LOG] Card effect: {_realCard.Description}");
            Debug.Log($"[LOG] Finished card: {_realCard.Name}");
        }
    }
    public T TryGetInterface<T>() where T : class
    {
        return _realCard as T;
    }

}
