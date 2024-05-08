using Presentation.Console.Resources;
using SeaBattle.Domain.GameBoard;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SeaBattle.PresentationConsole;

internal static class ConsoleTools
{
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

    public static string BoardToString(Board board)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < board.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BorderBottom);
        }
        sb.AppendLine();
        for (int y = 0; y < board.Height; y++)
        {
            sb.Append(Chars.BorderLeft);
            for (int x = 0; x < board.Width; x++)
            {
                var ceil = board.GetCeil((uint)x, (uint)y);
                if (ceil != null && ceil.ContainShip)
                {
                    sb.Append('X');
                }
                else
                {
                    sb.Append(' ');
                }
                if (x != board.Width - 1)
                    sb.Append(' ');
            }
            sb.Append(Chars.BorderRight);
            sb.AppendLine();
        }
        for (int i = 0; i < board.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BoarderTop);
        }

        return sb.ToString();
    }
}
