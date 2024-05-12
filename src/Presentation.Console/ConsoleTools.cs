using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SeaBattle.PresentationConsole;

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
    public static void SetConsoleSizeFromText(string text, int safeSpace = 5, bool setWidth = true, bool setHeight = true)
    {
        int maxLineLength = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                                .Max(line => line.Length);

        int consoleWidth = maxLineLength + 5;
        int consoleHeight = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length + 5;

        try
        {
            Console.SetWindowSize(
                setWidth ? consoleWidth : Console.WindowWidth,
                setHeight ? consoleWidth : Console.WindowHeight);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetBufferSize(
                    setWidth ? consoleWidth : Console.BufferWidth,
                    setHeight ? consoleHeight : Console.BufferHeight);
            }
        }
        catch (IOException e)
        {
            Debug.WriteLine($"[{nameof(ConsoleTools)}] {e.Message}");
        }
    }
}
