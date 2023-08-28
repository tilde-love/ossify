using System;

namespace Ossify.Microtypes
{
    [Serializable]
    public sealed class ByteArrayReference : Reference<byte[], ByteArrayVariable> { }
}