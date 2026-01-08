using System.Windows;
using BerryAIGen.Common;

namespace BerryAIGen.Toolkit.Services;

public class WindowService
{
    private Window _window;

    public Window CurrentWindow => _window;

    public void SetWindow(Window window)
    {
        _window = window;
    }
}







