using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private readonly List<TArtifact> artifacts = new ();

        public Custodian(Factory artifactFactory) => this.artifactFactory = artifactFactory;

        public int Count => artifacts.Count; 

        public TArtifact Get()
        {
            var result = artifactFactory(Remove);

            artifacts.Add(result); 

            return result;
        }

        public void Clear()
        {
            for (int index = artifacts.Count - 1; index >= 0; index--)
            {
                TArtifact license = artifacts[index];

                license.Dispose();
            }

            artifacts.Clear();
        }

        private void Remove(TArtifact artifact) => artifacts.Remove(artifact);

        /// <inheritdoc />
        public void Dispose() => Clear();

        /// <inheritdoc />
        public IEnumerator<TArtifact> GetEnumerator() => artifacts.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => (artifacts as IEnumerable).GetEnumerator();

        public TArtifact this[int i] => artifacts[i];
    }    

    public class Custodian<TArtifact, TData> : IDisposable, IEnumerable<TArtifact> where TArtifact : IArtifact
    {
        public delegate void Destructor(TArtifact artifact);
        public delegate TArtifact Factory(TData data, Destructor destructor);

        private readonly Factory artifactFactory;
        private readonly List<TArtifact> artifacts = new ();

        public Custodian(Factory artifactFactory) => this.artifactFactory = artifactFactory;

        public int Count => artifacts.Count; 

        public TArtifact Get(TData data)
        {
            var result = artifactFactory(data, Remove);

            artifacts.Add(result); 

            return result;
        }

        public void Clear()
        {
            for (int index = artifacts.Count - 1; index >= 0; index--)
            {
                TArtifact license = artifacts[index];

                license.Dispose();
            }

            artifacts.Clear();
        }

        private void Remove(TArtifact artifact) => artifacts.Remove(artifact);

        /// <inheritdoc />
        public void Dispose() => Clear();

        /// <inheritdoc />
        public IEnumerator<TArtifact> GetEnumerator() => artifacts.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => (artifacts as IEnumerable).GetEnumerator();

        public TArtifact this[int i] => artifacts[i];
    }    
}