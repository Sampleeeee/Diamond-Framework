namespace Diamond.Shared
{
    public static class Host
    {
        public static bool IsServer
        {
            get
            {
                #if SERVER
                    return true;
                #else
                    return false;
                #endif
            }
        }
    }
}