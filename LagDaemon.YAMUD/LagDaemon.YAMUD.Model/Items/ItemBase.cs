namespace LagDaemon.YAMUD.Model.Items;

public class Entity 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}


public class ItemBase : Entity
{
    public float Value { get; set; }
    public float Weight { get; set; }
    public int MaxStackSize { get; set; }
    public bool CanUse { get; set; }
    public bool CanConsume { get; set; }
    public bool CanSpoil { get; set; }
    public bool CanDestroy { get; set; }
    public bool CanDrop { get; set; }
    public bool CanPlace { get; set; }
    public bool  CanTake { get; set; }
}

public class InventorySlot
{
    public ItemBase Item { get; set; }
    public int Quantity { get; set; }
}

public class Inventory
{
    public ICollection<InventorySlot> Slots { get; set; }
}


public class Resource : ItemBase
{
    
}

