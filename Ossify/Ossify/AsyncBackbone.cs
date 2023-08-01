using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify
{
    public abstract class Backbone : ScriptableObject { }

    public abstract class AsyncBackbone : Backbone
    {
        public abstract UniTask PulseAsync(CancellationToken cancellationToken);
    }

    public abstract class CustodianBackbone : Backbone
    {
        private readonly Custodian<Artifact> custodian;

        protected CustodianBackbone() => custodian = new Custodian<Artifact>(CreateArtifact);

        private Artifact CreateArtifact(Custodian<Artifact>.Destructor destructor) => new(custodian.Count == 0 ? this : null, destructor);

        public Artifact Get() => custodian.Get();

        protected abstract void Begin();

        protected abstract void End();

        public sealed class Artifact : IArtifact
        {
            private readonly CustodianBackbone backbone;
            private readonly Custodian<Artifact>.Destructor destructor;

            internal Artifact(CustodianBackbone backbone, Custodian<Artifact>.Destructor destructor)
            {
                this.backbone = backbone;
                this.destructor = destructor;

                if (backbone != null) backbone.Begin();
            }

            /// <inheritdoc />
            public bool Disposed { get; private set; }

            /// <inheritdoc />
            public void Dispose()
            {
                if (Disposed) return;

                Disposed = true;

                if (backbone != null) backbone.End();

                destructor(this);
            }
        }
    }
}