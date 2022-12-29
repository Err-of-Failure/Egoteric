using Terraria.ModLoader;

namespace Egoteric.Common.Players
{
	/// <summary>
	/// All the keybinds used by this mod
	/// </summary>
	public class EgotericKeybinds : ModSystem
	{
		/// <summary>
		///		<para>
		///		Checks a players current stats
		///		</para>
		///		<para>
		///		Dev Keybind
		///		</para>
		/// </summary>
		public static ModKeybind checkCurrentStats { get; private set; }
		/// <summary>
		///		<para>
		///		Add a single level to a player
		///		</para>
		///		<para>
		///		Dev Keybind
		///		</para>
		/// </summary>
		public static ModKeybind addLevel { get; private set; }
		/// <summary>
		///		<para>
		///		Resets levels down to 1
		///		</para>
		///		<para>
		///		Dev Keybind
		///		</para> 
		/// </summary>
		public static ModKeybind resetLevel { get; private set; }
		/// <summary>
		///		<para>
		///		Opens and Closes UI
		///		</para>
		///		<para>
		///		Dev Keybind
		///		</para> 
		/// </summary>
		public static ModKeybind openUI { get; private set; }

		public override void Load()
		{
			checkCurrentStats = KeybindLoader.RegisterKeybind(Mod, "Check Test Stats", "K");
			addLevel = KeybindLoader.RegisterKeybind(Mod, "Add a Level", "L");
			resetLevel = KeybindLoader.RegisterKeybind(Mod, "Reset Levels", "OemSemicolon");

			openUI = KeybindLoader.RegisterKeybind(Mod, "Opens and Closes Level UI", "L");
		}

		public override void Unload()
		{
			checkCurrentStats = null;
			addLevel = null;
			resetLevel = null;

			openUI = null;
		}
	}
}
