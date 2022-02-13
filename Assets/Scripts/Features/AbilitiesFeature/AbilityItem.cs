using UnityEngine;

public class AbilityItem
{
    public int ItemID { get; }
    public GameObject View { get; }
    public AbilityType Type { get; }
    public Sprite ImageSprite { get; }
    public float Value { get; }
    public float Duration { get; }

    public AbilityItem(int itemID, GameObject view, AbilityType type, Sprite imageSprite, float value, float duration)
    {
        ItemID = itemID;
        View = view;
        Type = type;
        ImageSprite = imageSprite;
        Value = value;
        Duration = duration;
    }
}
