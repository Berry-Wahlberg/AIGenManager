using System;
using BerryAIGen.Common;

namespace BerryAIGen.Toolkit.Models;

[Flags]
public enum PopupButtons
{
    OK = 1,
    Cancel = 2,
    OkCancel = OK | Cancel,
    Yes = 4,
    No = 8,
    YesNo = Yes | No,
    None = 1024
}







