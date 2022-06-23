using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.DataStructures;
using System.Collections.Generic;
using ReLogic.Content;
using Terraria.ModLoader.IO;

namespace Overthrown.Content.NPCs
{
    [AutoloadHead]
    public class Torch : ModNPC
    {
        public int NumberOfTimesTalkedTo = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25;

            //NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
            //NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the npc that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = -1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) // Dislikes living near the merchant.
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) // Hates living near the demolitionist.
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Just a dragon that got interested in this pixelated world. Seems very keen to talking to people. Enjoys any company he gets."),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				new FlavorTextBestiaryInfoElement("Mods.Overthrown.Bestiary.Torch")
            });
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            // This code slowly rotates the NPC in the bestiary
            // (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
            if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
            {
                drawModifiers.Rotation += 0.001f;

                // Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }

            return true;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        { // Requirements for the town NPC to spawn.
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                // Player has to have either an ExampleItem or an ExampleBlock in order for the NPC to spawn
                if (player.inventory.Any(item => item.type == ModContent.ItemType<Items.Armor.Vanity.TorchHead>() || item.type == ModContent.ItemType<Items.Armor.Vanity.TorchHoodie>() || item.type == ModContent.ItemType<Items.Armor.Vanity.TorchShorts>()))
                {
                    return true;
                }
            }

            return false;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new TorchProfile();
        }
        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Torch"
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.PartyGirlDialogue", Main.npc[partyGirl].GivenName));
            }
            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.StandardDialogue1"));
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.StandardDialogue3"));
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.CommonDialogue"), 5.0);
            chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.RareDialogue"), 0.1);

            NumberOfTimesTalkedTo++;
            if (NumberOfTimesTalkedTo >= 10)
            {
                //This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
                chat.Add(Language.GetTextValue("Mods.Overthrown.Dialogue.Torch.TalkALot"));
            }

            return chat; // chat is implicitly cast to a string.
        }

        public override void SetChatButtons(ref string button, ref string button2)
        { // What the chat buttons are when you open up the chat UI
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Awesomeify";
            /*
            if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
            {
                button = "Upgrade " + Lang.GetItemNameValue(ItemID.HiveBackpack);
            }
            */
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                // We want 3 different functionalities for chat buttons, so we use HasItem to change button 1 between a shop and upgrade action.
                /*
                if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
                {
                    SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

                    Main.npcChatText = $"I upgraded your {Lang.GetItemNameValue(ItemID.HiveBackpack)} to a {Lang.GetItemNameValue(ModContent.ItemType<WaspNest>())}";

                    int hiveBackpackItemIndex = Main.LocalPlayer.FindItem(ItemID.HiveBackpack);
                    var entitySource = NPC.GetSource_GiftOrReward();

                    Main.LocalPlayer.inventory[hiveBackpackItemIndex].TurnToAir();
                    Main.LocalPlayer.QuickSpawnItem(entitySource, ModContent.ItemType<WaspNest>());

                    return;
                }
                */

                shop = true;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Armor.Vanity.TorchHead>());
            shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Armor.Vanity.TorchHoodie>());
            shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Items.Armor.Vanity.TorchShorts>());
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armor.Vanity.TorchHead>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armor.Vanity.TorchHoodie>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armor.Vanity.TorchShorts>()));
        }

        public override bool CanGoToStatue(bool toKingStatue) => true;

        public override void OnGoToStatue(bool toKingStatue)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)NPC.whoAmI);
                packet.Send();
            }
        }

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

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }


        //Have to comment these two because they don't work until the update
        /*
        public override void LoadData(TagCompound tag)
        {
            NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
        }
        */
    }
    public class TorchProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                return ModContent.Request<Texture2D>("Overthrown/Content/NPCs/Torch");

            if (npc.altTexture == 1)
                return ModContent.Request<Texture2D>("Overthrown/Content/NPCs/Torch_Party");

            return ModContent.Request<Texture2D>("Overthrown/Content/NPCs/Torch");
        }

        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("Overthrown/Content/NPCs/Torch_Head");
    }
}