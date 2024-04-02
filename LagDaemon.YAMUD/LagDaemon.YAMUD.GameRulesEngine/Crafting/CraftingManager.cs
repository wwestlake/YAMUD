using FluentResults;
using LagDaemon.YAMUD.Model.Items;

namespace LagDaemon.YAMUD.GameRulesEngine.Crafting
{
    // TODO: This needs to use the rules, and also needs to return better Result objects with details of failure

    public class CraftingManager
    {
        public Result CraftItem(CraftingRecipe recipe, Dictionary<Item, int> inventory)
            
        {
            var errors = new List<string>();

            // Step 1: Iterate through each required item in the recipe
            foreach (var requiredItem in recipe.RequiredItems)
            {
                // Step 2: Check if the inventory contains enough quantity
                if (!inventory.ContainsKey(requiredItem.Item) || inventory[requiredItem.Item] < requiredItem.Quantity)
                {
                    // Not enough of this item in the inventory, return false
                    errors.Add($"Meed more {requiredItem.Item.Name}");
                }
            }

            if (errors.Count > 0) return Result.Fail(errors);

            // Step 3: Inventory has enough of all required items, proceed with crafting
            // Step 4: Create the output item and add it to the inventory
            var outputItem = recipe.OutputItem;
            if (inventory.ContainsKey(outputItem))
            {
                inventory[outputItem]++;
            }
            else
            {
                inventory[outputItem] = 1;
            }

            // Step 5: Subtract the required quantity of each item from the inventory
            foreach (var requiredItem in recipe.RequiredItems)
            {
                inventory[requiredItem.Item] -= requiredItem.Quantity;
            }

            return Result.Ok();
        }
    }
}
