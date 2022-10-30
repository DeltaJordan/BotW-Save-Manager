﻿using System.Runtime.InteropServices;

namespace BotwSaveManager
{
    public static class Meta
    {
        public static string Name { get; } = "Botw Save Manager";
        public static string Version { get; } = "0.1.0-alpha";
        public static string Footer { get; } = $"{Name} - v{Version}";
        public static bool IsWindows { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
