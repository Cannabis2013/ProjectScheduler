using System;
using System.Collections.Generic;

namespace Templates
{
    [Serializable]
    public abstract partial class ModelEntity<T>
    {
        public abstract T ItemModel();
    }
}