using System.Drawing;
using CitizenFX.Core;

namespace Diamond.Client.Extensions
{
    public static class VectorExtensions
    {
        private const float _markerDistance = 30f * 30f;
        
        public static bool ShouldDrawMarker(this Vector3 pos, Vector3 other, out float distance)
        {
            distance = pos.DistanceToSquared(other);
            return distance < _markerDistance;   
        }
        
        public static bool ShouldDrawMarker(this Vector3 pos, Vector3 other) =>
            pos.DistanceToSquared(other) < _markerDistance;

        public static bool DrawMarkerHere(this Vector3 pos, out float distance, MarkerType markerType, Vector3? direction = null, Vector3? rotation = null, Vector3? scl = null, Color? clr = null)
        {
            distance = -1f;
            
            // ReSharper disable four ConvertToNullCoalescingCompoundAssignment
            direction = direction ?? Vector3.Zero;
            rotation = rotation ?? Vector3.Zero;
            scl = scl ?? Vector3.One;
            clr = clr ?? Color.FromArgb(255, 255, 255);

            var dir = (Vector3) direction;
            var rot = (Vector3) rotation;
            var scale = (Vector3) scl;
            var color = (Color) clr;

            if (!Game.PlayerPed.Position.ShouldDrawMarker(pos, out distance)) return false;
            
            World.DrawMarker(markerType, pos, dir, rot, scale, color);
            return true;
        }
    }
}