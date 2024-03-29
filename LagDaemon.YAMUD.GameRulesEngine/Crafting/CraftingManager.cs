using LagDaemon.YAMUD.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.GameRulesEngine.Crafting
{
    // TODO: This needs to use the rules, and also needs to return bwtter Result objects with details of failure

    public class CraftingManager
    {
        public bool CraftItem(CraftingRecipe recipe, Dictionary<Item, int> inventory)
        {
            // Step 1: Iterate through each required item in the recipe
            foreach (var requiredItem in recipe.RequiredItems)
            {
                // Step 2: Check if the inventory contains enough quantity
                if (!inventory.ContainsKey(requiredItem.Item) || inventory[requiredItem.Item] < requiredItem.Quantity)
                {
                    // Not enough of this item in the inventory, return false
                    return false;
                }
            }

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

            return true;
        }
    }
}
