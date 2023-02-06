using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;

namespace Oxide.Plugins
{
    [Info("EasyStreet", "Brady Gaster", "1.0.0")]
    [Description("Starts a new game with an inventory that sets you up for building a base and surviving the first night.")]
    class EasyStreet : CovalencePlugin
    {
        private const string LOG_FLAG = "<------------{ EASY STREET }------------>";
        private List<BasePlayer> _knownPlayers = new List<BasePlayer>();

        object OnDefaultItemsReceived(PlayerInventory inventory)
        {
            var player = inventory.baseEntity;

            if (!_knownPlayers.Any(x => x.displayName == player.displayName))
                _knownPlayers.Add(player);
            else
            {
                // we've seen this player before
                return null;
            }

            Puts(LOG_FLAG);
            Puts($"Let's welcome {player.displayName} to Easy Street with a nice starting inventory.");
            Puts(LOG_FLAG);

            // clear this player's inventory
            Puts(LOG_FLAG);
            inventory.containerBelt.Clear();
            Puts($"Cleared {player.displayName}'s starting inventory.");
            Puts(LOG_FLAG);

            // inventory
            foreach (var resource in new[] { "wood", "stones", "metal.fragments" })
            {
                for (int i = 0; i < 6; i++)
                {
                    AddItemToInventory(player, resource, 1000, player.inventory.containerMain);
                }
            }

            AddItemToInventory(player, "ammo.rifle", 128, player.inventory.containerMain);
            AddItemToInventory(player, "cupboard.tool", 1, player.inventory.containerMain);
            AddItemToInventory(player, "cloth", 50, player.inventory.containerMain);
            AddItemToInventory(player, "furnace", 1, player.inventory.containerMain);
            AddItemToInventory(player, "lowgradefuel", 20, player.inventory.containerMain);
            AddItemToInventory(player, "pumpkin", 20, player.inventory.containerMain);

            // belt
            AddItemToInventory(player, "rifle.lr300", 1, player.inventory.containerBelt);
            AddItemToInventory(player, "knife.combat", 1, player.inventory.containerBelt);
            AddItemToInventory(player, "chainsaw", 1, player.inventory.containerBelt);
            AddItemToInventory(player, "jackhammer", 1, player.inventory.containerBelt);
            AddItemToInventory(player, "hammer", 1, player.inventory.containerBelt);
            AddItemToInventory(player, "building.planner", 1, player.inventory.containerBelt);

            // clothing
            AddItemToInventory(player, "hat.miner", 1, player.inventory.containerWear);
            AddItemToInventory(player, "tshirt.long", 1, player.inventory.containerWear);
            AddItemToInventory(player, "roadsign.jacket", 1, player.inventory.containerWear);
            AddItemToInventory(player, "pants", 1, player.inventory.containerWear);
            AddItemToInventory(player, "shoes.boots", 1, player.inventory.containerWear);
            AddItemToInventory(player, "mask.bandana", 1, player.inventory.containerWear);
            
            Puts(LOG_FLAG);
            Puts($"{player.displayName} welcomed to Easy Street with a nice starting inventory.");
            Puts(LOG_FLAG);


            return null;
        }

        void AddItemToInventory(BasePlayer player, string itemName, int amount, ItemContainer container)
        {
            Puts($"Adding {itemName}");
            player.inventory.GiveItem(ItemManager.CreateByName(itemName, amount), container);
        }
    }
}