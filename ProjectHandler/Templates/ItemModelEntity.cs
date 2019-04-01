namespace Templates
{
    public abstract class ItemModelEntity<T>
    {
        public virtual string title
        {
            get => t;
            set => t = value;
        }

        public abstract T itemModel();

        protected string t;
    }
}
