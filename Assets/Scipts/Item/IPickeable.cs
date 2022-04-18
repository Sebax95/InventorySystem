public interface IPickeable
{
    public Item Item { get; }
    public void Pick(Inventory inv);
}
