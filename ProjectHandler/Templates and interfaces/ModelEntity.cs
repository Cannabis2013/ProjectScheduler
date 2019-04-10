using System;

namespace Templates
{
    [Serializable]
    public abstract class ModelEntity<T>
    {
        protected string itemId;

        public enum ListMode
        {
            Tile,
            List
        }

        public virtual string id
        {
            get => itemId;
            set => itemId = value;
        }
        
        public abstract T ItemModel(ListMode mode = ListMode.Tile);
    }
}