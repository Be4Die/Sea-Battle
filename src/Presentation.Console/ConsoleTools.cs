using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SeaBattle.Presentation.Console;

/// <summary>
/// Provides utility methods for console applications.
/// </summary>
internal static class ConsoleTools
{
    /// <summary>
    /// Sets the console window size based on the provided text.
    /// </summary>
    /// <param name="text">The text to base the console size on.</param>
    /// <param name="safeSpace">The additional space to add to the width and height.</param>
    /// <param name="setWidth">Whether to set the width of the console window.</param>
    /// <param name="setHeight">Whether to set the height of the console window.</param>
    public static void SetConsoleSizeFromText(string text, bool setWidth = true, bool setHeight = true)
    {
        int maxLineLength = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                                .Max(line => line.Length);

        int consoleWidth = maxLineLength + 5;
        int consoleHeight = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length + 5;

        try
        {
            System.Console.SetWindowSize(
                setWidth ? consoleWidth : System.Console.WindowWidth,
                setHeight ? consoleWidth : System.Console.WindowHeight);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                System.Console.SetBufferSize(
                    setWidth ? consoleWidth : System.Console.BufferWidth,
                    setHeight ? consoleHeight : System.Console.BufferHeight);
            }
        }
        catch (IOException e)
        {
            Debug.WriteLine($"[{nameof(ConsoleTools)}] {e.Message}");
        }
    }
}
