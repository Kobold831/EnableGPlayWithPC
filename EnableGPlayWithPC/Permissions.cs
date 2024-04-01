namespace EnableGPlayWithPC {
    internal static class Permissions {
        internal static string Prefix = "android.permission.";

        internal static string[][] PermissionGroups = [
            
            // GoogleServicesFramework
            [
                "DUMP",
                "READ_LOGS",
                "WRITE_SECURE_SETTINGS",
                "INTERACT_ACROSS_USERS"
            ],
            
            // GmsCore
            [
                "INTERACT_ACROSS_USERS",
                "PACKAGE_USAGE_STATS",
                "GET_APP_OPS_STATS",
                "READ_LOGS"
            ],
            
            // Phonesky
            [
                "PACKAGE_USAGE_STATS",
                "BATTERY_STATS",
                "DUMP",
                "GET_APP_OPS_STATS",
                "INTERACT_ACROSS_USERS",
                "WRITE_SECURE_SETTINGS"
            ]

        ];
    }
}
