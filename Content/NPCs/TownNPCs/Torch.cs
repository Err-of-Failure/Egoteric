using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using Terraria.GameContent;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Overthrown.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Torch : ModNPC
    {
        public int NumberOfTimesTalkedTo = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torch");
            Main.npcFrameCount[Type] = 26;
            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 5;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 2;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = -1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 280;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                new FlavorTextBestiaryInfoElement("Just a dragon that got interested in this pixelated world. Seems very keen to talking to people. Enjoys any company he gets.")
            });
        }

        public override void OnKill()
        {
            //Edit this once "Gore" is made
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (NPC.downedBoss1 && NPC.downedSlimeKing)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new TorchProfile();
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.PartyGirlDialogue", Main.npc[partyGirl].GivenName));
            }
            int guide = NPC.FindFirstNPC(NPCID.Guide);
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.StandardDialogue1"));
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.StandardDialogue3", Main.npc[guide].GivenName));
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.CommonDialogue"), 5.0);
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.RareDialogue"), 0.1);

            NumberOfTimesTalkedTo++;
            if (NumberOfTimesTalkedTo >= 10)
            {
                chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.TalkALot"));
            }

            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Armor.Vanity.TorchHead>());
            shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Armor.Vanity.TorchHoodie>());
            shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Armor.Vanity.TorchShorts>());
        }

        public override bool CanGoToStatue(bool toKingStatue) => true;

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.FireArrow;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
    public class TorchProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => null;

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                return ModContent.Request<Texture2D>("Overthrown/Content/NPCs/TownNPCs/Torch");

            if (npc.altTexture == 1)
                return ModContent.Request<Texture2D>("Overthrown/Content/NPCs/TownNPCs/Torch_Party");

            return ModContent.Request<Texture2D>("Overthrown/Content/NPCs/TownNPCs/Torch");
        }

        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("Overthrown/Content/NPCs/TownNPCs/Torch_Head");
    }
}
