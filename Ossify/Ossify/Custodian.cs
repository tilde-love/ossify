using System;
using System.Collections;
using System.Collections.Generic;

namespace Ossify
{
    public interface IArtifact : IDisposable
    {
        bool Disposed { get; }
    }

    public class Custodian<TArtifact> : IDisposable, IEnumerable<TArtifact> where TArtifact : IArtifact
    {
        public delegate void Destructor(TArtifact artifact);

        public delegate TArtifact Factory(Destructor destructor);

        private readonly Factory artifactFactory;

        private readonly List<TArtifact> artifacts = new();

        public TArtifact this[int i] => artifacts[i];

        public int Count => artifacts.Count;

        public Custodian(Factory artifactFactory) => this.artifactFactory = artifactFactory;

        /// <inheritdoc />
        public void Dispose() => Clear();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => (artifacts as IEnumerable).GetEnumerator();

        /// <inheritdoc />
        public IEnumerator<TArtifact> GetEnumerator() => artifacts.GetEnumerator();

        public void Clear()
        {
            for (int index = artifacts.Count - 1; index >= 0; index--)
            {
                TArtifact license = artifacts[index];

                license.Dispose();
            }

            artifacts.Clear();
        }

        public TArtifact Get()
        {
            TArtifact result = artifactFactory(Remove);

            artifacts.Add(result);

            return result;
        }

        private void Remove(TArtifact artifact) => artifacts.Remove(artifact);
    }

    public class Custodian<TArtifact, TData> : IDisposable, IEnumerable<TArtifact> where TArtifact : IArtifact
    {
        public delegate void Destructor(TArtifact artifact);

        public delegate TArtifact Factory(TData data, Destructor destructor);

        private readonly Factory artifactFactory;

        private readonly List<TArtifact> artifacts = new();

        public TArtifact this[int i] => artifacts[i];

        public int Count => artifacts.Count;

        public Custodian(Factory artifactFactory) => this.artifactFactory = artifactFactory;

        /// <inheritdoc />
        public void Dispose() => Clear();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => (artifacts as IEnumerable).GetEnumerator();

        /// <inheritdoc />
        public IEnumerator<TArtifact> GetEnumerator() => artifacts.GetEnumerator();

        public void Clear()
        {
            for (int index = artifacts.Count - 1; index >= 0; index--)
            {
                TArtifact license = artifacts[index];

                license.Dispose();
            }

            artifacts.Clear();
        }

        public TArtifact Get(TData data)
        {
            TArtifact result = artifactFactory(data, Remove);

            artifacts.Add(result);

            return result;
        }

        private void Remove(TArtifact artifact) => artifacts.Remove(artifact);
    }
}