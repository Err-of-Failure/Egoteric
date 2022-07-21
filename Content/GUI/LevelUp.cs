using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Egoteric.Content.GUI
{
	internal class LevelUp : UIState
	{
		public static bool Visible;

		private UIPanel levelBackground;
		private UIPanel separator1;
		private UIText levelHeader;
		private UIImage levelBarImage;
		private UIFlatPanel expBar;

		public override void OnInitialize()
		{
			levelBackground = new UIPanel();
			levelBackground.Width.Set(0f, 0.4f);
			levelBackground.Height.Set(0f, 0.3f);
			levelBackground.VAlign = 0.5f;
			levelBackground.HAlign = 0.5f;
			levelBackground.BackgroundColor = new Color(0, 0, 0, 155);
			levelBackground.BorderColor = new Color(0, 0, 0, 255);
			Append(levelBackground);

			separator1 = new UIPanel();
			separator1.Top.Set(0f, 0.15f);
			separator1.Width.Set(0f, 1f);
			separator1.Height.Set(0f, 0.1f);
			separator1.BackgroundColor = new Color(44, 0, 0, 155);
			separator1.BorderColor = new Color(0, 0, 0, 255);
			levelBackground.Append(separator1);

			levelHeader = new UIText("Levels", 1.5f, false);
			levelHeader.Width.Set(0f, 1f);
			levelHeader.MinWidth.Set(82f, 0f);
			levelBackground.Append(levelHeader);

			levelBarImage = new UIImage(ModContent.Request<Texture2D>("Egoteric/Content/GUI/RedPlus"));
			levelBarImage.Top.Set(0f, 0.25f);
			levelBarImage.Left.Set(0f, 0.02f);
			levelBarImage.Width.Set(32f, 0f);
			levelBarImage.Height.Set(32f, 0f);
			levelBackground.Append(levelBarImage);

			expBar = new UIFlatPanel();
			expBar.Top.Set(0f, 0.25f);
			expBar.Left.Set(levelBarImage.Width.Pixels, 0f);
			expBar.backgroundColor = Color.Green;
			levelBackground.Append(expBar);
		}
	}
	class UIFlatPanel : UIElement
    {
		public Color backgroundColor = Color.Gray;
		private static Texture2D _backgroundTexture;

		public UIFlatPanel()
		{
			if (_backgroundTexture == null)
				_backgroundTexture = (Texture2D)ModContent.Request<Texture2D>("CustomBars/Textures/UI/Blank");
		}
	}

	internal enum LevelStuff
    {
		level,
		curEXP,
		maxEXP,
		skillPoints
    }
	
	class LevelBar : UIElement
    {

    }
}
