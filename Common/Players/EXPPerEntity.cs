using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Egoteric.Common.Players;
using Egoteric.Common.Configs;

namespace Egoteric.Common.Players
{
    public class EXPPerEntity : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);

            float XPToEarn = 0f;

            if (npc.type != NPCID.TargetDummy && !npc.SpawnedFromStatue && !npc.friendly && !npc.townNPC)
            {
                if (npc.boss)
                {
                    // XPToEarn = (npc.lifeMax * ModContent.GetInstance<MainConfig>().BossMultiplier);
					XPToEarn = (npc.lifeMax * 1.5f);
                }
                else if (!npc.friendly)
                {
                    // XPToEarn = (npc.lifeMax * ModContent.GetInstance<MainConfig>().NormalMultiplier);
					XPToEarn = (npc.lifeMax * 1f);
                }
                else if (npc.friendly)
                {
                    // XPToEarn = (npc.lifeMax * ModContent.GetInstance<MainConfig>().EvilMultiplier);
					XPToEarn = (npc.lifeMax * -1f);
                }
            }

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.LocalPlayer.GetModPlayer<EgotericPlayer>().AddXP((int)XPToEarn);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                for (int i = 0; i < npc.playerInteraction.Length; ++i)
                {
                    if (npc.playerInteraction[i])
                    {
                        ModPacket packet = Egoteric.Instance.GetPacket();
                        packet.Write((byte)Egoteric.MessageType.XP);
                        packet.Write(XPToEarn);
                        packet.Send(i);
                    }
                }
            }
        }
    }
}
