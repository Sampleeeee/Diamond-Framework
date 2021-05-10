using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Diamond.Client.Extensions
{
    public static class EntityExtensions
    {
        public static float GetSpeed(this Entity entity) =>
            API.GetEntitySpeed(entity.Handle);

        public static Vector3 GetSpeedVector(this Entity entity, bool relative = true) =>
            API.GetEntitySpeedVector(entity.Handle, relative);
        
        public static Vector3 GetOffsetInWorldCoords(this Entity entity, float x, float y, float z) =>
            API.GetOffsetFromEntityInWorldCoords(entity.Handle, x, y, z);
    }
}