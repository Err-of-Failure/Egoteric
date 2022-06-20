using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Overthrown.Content.DamageClasses;

namespace Overthrown.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class VoidHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Helmet");
            Tooltip.SetDefault("Beta Item");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            
        }

        public override void UpdateArmorSet(Player player)
        {
            
        }


    }
}
