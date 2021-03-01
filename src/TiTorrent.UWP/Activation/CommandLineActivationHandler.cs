using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace TiTorrent.UWP.Activation
{
    internal class CommandLineActivationHandler : ActivationHandler<CommandLineActivatedEventArgs>
    {
        protected override async Task HandleInternalAsync(CommandLineActivatedEventArgs args)
        {
            var operation = args.Operation;
            var cmdLineString = operation.Arguments;
            var activationPath = operation.CurrentDirectoryPath;
            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(CommandLineActivatedEventArgs args)
        {
            return args?.Operation.Arguments.Any() ?? false;
        }
    }
}
