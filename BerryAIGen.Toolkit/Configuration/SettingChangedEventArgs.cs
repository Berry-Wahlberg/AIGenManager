namespace BerryAIGen.Toolkit.Configuration;
using BerryAIGen.Common;

public class SettingChangedEventArgs
{
    public string SettingName { get; set; }
    public object? OldValue { get; set; }
    public object? NewValue { get; set; }
}






