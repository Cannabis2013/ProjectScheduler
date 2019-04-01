namespace Templates
{
    public abstract class ItemModelEntity<T>
    {
        public virtual string title
        {
            get => t;
            set => t = value;
        }

        public abstract T itemModel(ListMode mode = ListMode.Tile);

        protected string t;

        public enum ListMode { Tile, List};
    }
}
