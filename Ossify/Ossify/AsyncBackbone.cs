using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ossify
{
    public abstract class AsyncBackbone : Backbone
    {
        public abstract UniTask PulseAsync(CancellationToken cancellationToken);
    }
}