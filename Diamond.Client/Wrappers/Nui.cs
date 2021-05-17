using System.ComponentModel;
using System.Security.Permissions;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Diamond.Client.Wrappers
{
	public class Nui
	{
		private static bool _hasFocus = false;
		private static bool _hasCursor = false;

		private static bool _keepInput = false;

		public static void DisableFocus()
		{
			API.SetNuiFocus(false, false);
			API.SetNuiFocusKeepInput(false);

			_hasFocus = false;
			_hasCursor = false;

			if (_keepInput)
				Game.Player.CanControlCharacter = true;

			_keepInput = false;
		}

		public static void EnableFocus( bool cursor = true, bool input = false )
		{
			API.SetNuiFocus(true, cursor);
			API.SetNuiFocusKeepInput( input );
			
			_hasFocus = true;
			_hasCursor = cursor;
			_keepInput = input;

			if ( input ) Game.Player.CanControlCharacter = false;
		}

		public static bool HasFocus(bool cursor = false, bool input = false)
		{
			if ( !_hasFocus ) return false;
			if ( cursor && !_hasCursor ) return false;
			if ( input && !_keepInput ) return false;
			
			return true;
		}
	}
}
