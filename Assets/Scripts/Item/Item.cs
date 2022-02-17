public class Item<T> : IItem
{
    public int ItemID { get; }
    public T ItemProperty { get;}

    public Item(T itemProperty, int itemID)
    {
        ItemProperty = itemProperty;
        ItemID = itemID;
    }

    public T1 GetItemProperty<T1>() where T1: class
    {
        return ItemProperty as T1;
    }
}
