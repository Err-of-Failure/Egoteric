using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Graphics;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Default;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Egoteric.Common.Players;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using ReLogic.Content;

namespace Egoteric.Content.GUI
{
	internal enum ResourceBarInfo
	{
		level,
		curEXP,
		maxEXP,
		skillPoints
	}
	class UIFlatPanel : UIElement
	{
		public Color backgroundColor = Color.Gray;
		public static Asset<Texture2D> _backgroundTexture;

		public UIFlatPanel(Asset<Texture2D> texture = null)
		{
			_backgroundTexture = texture;
			if (_backgroundTexture == null)
				_backgroundTexture = ModContent.Request<Texture2D>("Egoteric/Content/GUI/Blank");
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetDimensions();
			Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
			int width = (int)Math.Ceiling(dimensions.Width);
			int height = (int)Math.Ceiling(dimensions.Height);
			spriteBatch.Draw((Texture2D)_backgroundTexture, new Rectangle(point1.X, point1.Y, width, height), backgroundColor);
		}
	}
	class ResourceBar : UIElement
	{
		private ResourceBarInfo stat;
		private float width;
		private float height;
		private Asset<Texture2D> BarTexture;

		public ResourceBar(ResourceBarInfo stat, float width, float height, Asset<Texture2D> barTexture = null)
		{
			this.stat = stat;
			this.width = width;
			this.height = height;
			this.BarTexture = barTexture;
			if (this.BarTexture == null)
				this.BarTexture = ModContent.Request<Texture2D>("Egoteric/Content/GUI/Blank");
		}

		private UIFlatPanel currentBar;
		private UIFlatPanel barBackground;
		private UIText text;

		public override void OnInitialize()
		{
			Height.Set(height, 0f);
			Width.Set(width, 0f);

			barBackground = new UIFlatPanel(BarTexture);
			barBackground.Left.Set(0f, 0f);
			barBackground.Top.Set(0f, 0f);
			barBackground.backgroundColor = Color.Gray;
			barBackground.Width.Set(width, 0f);
			barBackground.Height.Set(height, 0f);

			currentBar = new UIFlatPanel(BarTexture);
			currentBar.SetPadding(0);
			currentBar.Left.Set(0f, 0f);
			currentBar.Top.Set(0f, 0f);
			currentBar.Width.Set(width, 0f);
			currentBar.Height.Set(height, 0f);

			switch (stat)
			{
				case ResourceBarInfo.curEXP:
					currentBar.backgroundColor = Color.Green;
					break;

				default:
					break;
			}

			text = new UIText("0|0");
			text.Width.Set(width, 0f);
			text.Height.Set(height, 0f);
			text.Top.Set(height / 2 - text.MinHeight.Pixels / 2, 0f);

			barBackground.Append(currentBar);
			barBackground.Append(text);
			base.Append(barBackground);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Player player = Main.player[Main.myPlayer];
			EgotericPlayer egotericPlayer = player.GetModPlayer<EgotericPlayer>();
			float quotient = 1f;
			switch (stat)
			{
				case ResourceBarInfo.curEXP:
					// quotient = (float)egotericPlayer.curEXP / (float)egotericPlayer.maxEXP;
					quotient = 1 / 2;
					break;

				default:
					break;
			}
			currentBar.Width.Set(quotient * width, 0f);
			Recalculate();

			base.Draw(spriteBatch);
		}
		public override void Update(GameTime gameTime)
		{
			Player player = Main.player[Main.myPlayer];
			EgotericPlayer egotericPlayer = player.GetModPlayer<EgotericPlayer>();
			switch (stat)
			{
				case ResourceBarInfo.curEXP:
					// text.SetText("" + egotericPlayer.curEXP + " | " + egotericPlayer.maxEXP);
					text.SetText("This isn't permanent");
					break;

				default:
					break;
			}
			base.Update(gameTime);
		}
	}
}
