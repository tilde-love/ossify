using System;

namespace Ossify.Variables
{
    [Serializable]
    public sealed class ByteArrayReference : Reference<byte[], ByteArrayVariable>
    {
    }
}