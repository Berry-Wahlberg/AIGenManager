using System.Threading.Tasks;
using BerryAIGen.Common;
using System.Windows.Input;

namespace BerryAIGen.Toolkit.Common;

public interface IAsyncCommand<in T> : ICommand
{
    Task ExecuteAsync(T? parameter);
    bool CanExecute(T? parameter);
}







