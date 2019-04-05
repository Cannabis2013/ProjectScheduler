using System;

namespace Templates
{
    [Serializable()]
    public abstract class ItemModelEntity<T>
    {
        public enum ListMode
        {
            Tile,
            List
        }

        protected string t;

        public virtual string id
        {
            get => t;
            set => t = value;
        }

        public abstract T itemModel(ListMode mode = ListMode.Tile);
    }
}