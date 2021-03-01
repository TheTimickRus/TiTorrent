using System.Threading.Tasks;

namespace TiTorrent.UWP.Activation
{
    internal abstract class ActivationHandler
    {
        public abstract bool CanHandle(object args);
        public abstract Task HandleAsync(object args);
    }

    internal abstract class ActivationHandler<T> : ActivationHandler where T : class
    {
        protected abstract Task HandleInternalAsync(T args);

        public override async Task HandleAsync(object args)
        {
            await HandleInternalAsync(args as T);
        }

        public override bool CanHandle(object args)
        {
            return args is T && CanHandleInternal((T) args);
        }

        protected virtual bool CanHandleInternal(T args)
        {
            return true;
        }
    }
}
