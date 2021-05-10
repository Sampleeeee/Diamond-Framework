using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace Diamond.Client
{
    public static class Utility
    {
        private const float MarkerDistance = 30 * 30;
        
        private static Dictionary<string, int> _hashList = new Dictionary<string, int>();
        
        public static void DrawText(string text, float x, float y, float scale = 0.4f, Font font = Font.ChaletLondon, bool outline = true)
        {
            API.SetTextFont((int) font);
            
            if (outline)
                API.SetTextOutline();
            
            API.SetTextEntry("STRING");
            API.AddTextComponentSubstringPlayerName(text);
            API.DrawText(x, y);
        }

        public static int GetHashKey(string model)
        {
            if (!_hashList.ContainsKey(model))
                _hashList.Add(model, API.GetHashKey(model));

            return _hashList[model];
        }
        
        // TODO fix this
        // https://github.com/citizenfx/fivem/blob/master/code/client/clrcore/External/DlcWeaponStructs.cs
        public static Tuple<int, int> GetProperTorso(Ped ped, int drawable, int texture)
        {
            int model = ped.Model.Hash;

            if (model != GetHashKey("mp_m_freemode_01") && model != GetHashKey("mp_f_freemode_01"))
                return new Tuple<int, int>(-1, -1);

            var topHash = (uint) API.GetHashNameForComponent(ped.Handle, 11, drawable, texture);
            int fcTorsoDrawable = -1, fcTorsoTexture = -1;

            for (int i = 0; i < API.GetNumForcedComponents(topHash); i++)
            {
                int fcNameHash = -1, fcEnumValue = -1, fcType = -1;
                API.GetForcedComponent(topHash, i, ref fcNameHash, ref fcEnumValue, ref fcType);

                if (fcType == 3)
                {
                    if (fcNameHash == 0 || fcNameHash == GetHashKey("0"))
                    {
                        fcTorsoDrawable = fcEnumValue;
                        fcTorsoTexture = 0;
                    }
                    else
                    {
                        int outComponent = -1;
                        API.GetShopPedComponent((uint) fcNameHash, ref outComponent);

                        // TODO figure out how to do this
                        // fcTorsoDrawable = torsoData.drawable;
                        // fcTorsoTexture = torsoData.texture;
                    }
                }
            }

            return new Tuple<int, int>(fcTorsoDrawable, fcTorsoTexture);
        }
    }
}