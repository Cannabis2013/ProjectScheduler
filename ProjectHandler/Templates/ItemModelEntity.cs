using System;

namespace Projecthandler.Templates
{
    [Serializable]
    public abstract class ItemModelEntity<T>
    {
        public enum ListMode
        {
            Tile,
            List
        }

        public string EntityTitle;

        public virtual string Id
        {
            get => EntityTitle;
            set => EntityTitle = value;
        }

        public abstract T ItemModel(ListMode mode = ListMode.Tile);
    }
}