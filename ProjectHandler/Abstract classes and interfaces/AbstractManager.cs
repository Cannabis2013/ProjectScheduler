using System;
using System.Collections.Generic;

namespace Templates
{
    [Serializable]
    public abstract class AbstractManager<ItemType, ModelType>
    {
        protected List<ItemType> ModelList = new List<ItemType>();

        public void AddModel(ItemType item) => ModelList.Add(item);

        public void RemoveModel(ItemType item) => ModelList?.Remove(item);
        public void RemoveModelAt(int index) => ModelList?.RemoveAt(index);

        public ItemType Model(ItemType item) => ModelList.Find(i => i.Equals(item));
        public abstract ItemType Model(string id);
        public ItemType ModelAt(int index) => ModelList[index];

        public abstract List<string> ListModelIdentities();

        public virtual ModelType[] ModelListViewItem()
        {
            throw new NotImplementedException();
        }
    }

}
