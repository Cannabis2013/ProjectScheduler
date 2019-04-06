using System;

namespace Templates
{
    [Serializable]
    public abstract class ItemModelEntity<T>
    {
        public enum ListMode
        {
            Tile,
            List
        }

        protected string itemId;

        public virtual string id
        {
            get => itemId;
            set => itemId = value;
        }

        public abstract T ItemModel(ListMode mode = ListMode.Tile);
    }
}