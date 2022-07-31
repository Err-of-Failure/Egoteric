using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Graphics;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Default;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Egoteric.Common.Players;
using Egoteric.Content.GUI;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using ReLogic.Content;

namespace Egoteric.Content.GUI
{
	internal class LevelsScreen : UIState
	{
		public static bool Visible;
		public static bool Added;

		private static UIPanel levelBackground;
		private UIText title;
		private UIHorizontalSeparator seperator1;
		private UIImage XPimage;
		private ResourceBar testBar;
		public static UICharacter playerImage;

		public override void OnInitialize()
		{
			levelBackground = new UIPanel();
			levelBackground.Width.Set(800f, 0f);
			levelBackground.Height.Set(400f, 0f);
			levelBackground.VAlign = 0.5f;
			levelBackground.HAlign = 0.5f;
			levelBackground.BackgroundColor = new Color(0, 0, 0, 155);
			levelBackground.BorderColor = new Color(0, 0, 0, 255);
			levelBackground.OnMouseDown += new UIElement.MouseEvent(DragStart);
			levelBackground.OnMouseUp += new UIElement.MouseEvent(DragEnd);
			Append(levelBackground);

			seperator1 = new UIHorizontalSeparator(1);
			seperator1.Width.Set(0f, 1f);
			seperator1.Height.Set(16f, 0f);
			seperator1.Top.Set(0f, 0.15f);
			seperator1.Color = new Color(44, 0, 0, 255);
			levelBackground.Append(seperator1);

			XPimage = new UIImage(ModContent.Request<Texture2D>("Egoteric/Content/GUI/XP"));
			XPimage.Top.Set(0f, 0.25f);
			XPimage.Left.Set(0f, 0f);
			levelBackground.Append(XPimage);

			testBar = new ResourceBar(ResourceBarInfo.curEXP, 600f, 28f, ModContent.Request<Texture2D>("Egoteric/Content/GUI/bar300x16"));
			testBar.Left.Set(40f, 0f);
			testBar.Top.Set(2f, 0.25f);
			levelBackground.Append(testBar);

			title = new UIText("Levels", 2);
			title.Top.Set(0f, 0f);
			title.Left.Set(0f, 0f);
			title.Width.Set(0f, 1f);
			title.Height.Set(0f, 0.15f);
			levelBackground.Append(title);

			// I need help with this
			/*
			playerImage = new UICharacter(Main.player[activatingPlayer], false, true);
			playerImage.Left.Set(650f, 0f);
			playerImage.Top.Set(0f, 0.25f);
			playerImage.Width.Set(100f, 0f);
			playerImage.Height.Set(200f, 0f);
			levelBackground.Append(playerImage);
			*/
		}

		public static void AddCharacterImage(Player player)
		{
			Added = true;
			playerImage = new UICharacter(player, false, false);
			playerImage.Left.Set(650f, 0f);
			playerImage.Top.Set(0f, 0.25f);
			playerImage.Width.Set(116f, 0f);
			playerImage.Height.Set(118f, 0f);
			levelBackground.Append(playerImage);
		}

		public static void RemoveCharacterImage(UIElement element)
		{
			Added = false;
			levelBackground.RemoveChild(element);
		}

		private Vector2 offset;
		public bool dragging = false;

		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
        {
			offset = new Vector2(evt.MousePosition.X - levelBackground.Left.Pixels, evt.MousePosition.Y - levelBackground.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
        {
			Vector2 end = evt.MousePosition;
			dragging = false;

			levelBackground.Left.Set(end.X - offset.X, 0f);
			levelBackground.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}

        public override void Update(GameTime gameTime)
        {
			if (dragging)
			{
				levelBackground.Left.Set(Main.mouseX - offset.X, 0f); 
				levelBackground.Top.Set(Main.mouseY - offset.Y, 0f);
				Recalculate();
			}
			base.Update(gameTime);
        }
    }
}
