namespace RAY
{
    internal static class FileInfo
    {
        private static string CONFIG = "config.cfg";
        private static string COLORS = @"Clut\colors.pal";
        private static string LET2 = @"Clut\let2.pal";
        private static string RAY_XXX = "RAY.XXX";
        private static string LET2_IMG = "LET2.IMG";
        private static string EXE = @"SLES_000.49";
        private static string RAYUS = @"rayus.txt";
        private static string CHARS = @"chars.cfg";

        public static string Config { get { return CONFIG; } }
        public static string Colors { get { return COLORS; } }
        public static string Let2 { get { return LET2; } }
        public static string RayXXX { get { return $@"{RAY.WorkspacePath}\{RAY_XXX}"; } }
        public static string Let2Img { get { return $@"{RAY.WorkspacePath}\{LET2_IMG}"; } }
        public static string Exe { get { return $@"{RAY.WorkspacePath}\{EXE}"; } }
        public static string Rayus { get { return $@"{RAY.WorkspacePath}\{RAYUS}"; } }
        public static string Chars { get { return $@"{RAY.WorkspacePath}\{CHARS}"; } }
    }
}
