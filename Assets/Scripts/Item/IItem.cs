public interface IItem
{
    int ItemID { get; }
    T GetItemProperty<T>() where T : class;    
}
