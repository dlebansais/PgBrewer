namespace WpfLayout;

using System.Diagnostics;
using System.Reflection;

public static class SystemTools
{
    #region Init
    static SystemTools()
    {
        IsRunningInBrowser = false;
        IsInitialized = true;
        IsExecutingOnWindows = true;
        AppVersion = string.Empty;
    }
    #endregion

    #region Properties
    public static bool IsRunningInBrowser { get; }
    public static bool IsInitialized { get; }
    public static bool IsExecutingOnWindows { get; }
    public static string AppVersion { get; }
    public static bool IsAppVersionKnown { get { return AppVersion.Length > 0; } }
    public static bool IsGlobalizationInvariantMode { get { return new string(new char[] { '\u0063', '\u0301', '\u0327', '\u00BE' }).IsNormalized(); } }
    #endregion

    #region Client Interface
    public static string? GetVersion()
    {
        Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
#pragma warning disable IL3000 // Avoid using accessing Assembly file path when publishing as a single-file
        FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(CurrentAssembly.Location);
#pragma warning restore IL3000 // Avoid using accessing Assembly file path when publishing as a single-file

        return VersionInfo.FileVersion;
    }
    #endregion
}
