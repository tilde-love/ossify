using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ossify
{
    public abstract class Backbone : ScriptableObject 
    {
    }

    public abstract class AsyncBackbone : Backbone
    {
        public abstract UniTask PulseAsync(CancellationToken cancellationToken);
    }

    public abstract class CustodianBackbone : Backbone
    {
        public sealed class Artifact : IArtifact  
        {
            private readonly CustodianBackbone backbone;
            private bool disposed;
            private readonly Custodian<Artifact>.Destructor destructor;

            internal Artifact(CustodianBackbone backbone, Custodian<Artifact>.Destructor destructor)
            {
                this.backbone = backbone;
                this.destructor = destructor;

                if (backbone != null) backbone.Begin();
            }

            /// <inheritdoc />
            public void Dispose()
            {
                if (disposed) return;

                disposed = true;

                if (backbone != null) backbone.End();
   
                destructor(this);
            }

            /// <inheritdoc />
            public bool Disposed => disposed; 
        }

        readonly Custodian<Artifact> custodian;

        public CustodianBackbone() => custodian = new Custodian<Artifact>(CreateArtifact);

        private Artifact CreateArtifact(Custodian<Artifact>.Destructor destructor) => new(custodian.Count == 0 ? this : null, destructor);

        public Artifact Get() => custodian.Get();

        protected abstract void Begin();

        protected abstract void End();
    }
}