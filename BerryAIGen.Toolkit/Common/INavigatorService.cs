
using System.Windows.Controls;
using BerryAIGen.Common;

namespace BerryAIGen.Toolkit.Common;

public interface INavigatorService
{
    void Goto(string url);
    void Back();
    void RegisterRoute(string path, Page page);

}







