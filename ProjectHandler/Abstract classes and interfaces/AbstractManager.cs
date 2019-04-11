using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Templates;

namespace Projecthandler.Templates_and_interfaces
{
    [Serializable]
    public abstract class AbstractModel  <ItemModelType,SubModelType> : ModelEntity<ItemModelType>
    {
        private string ModelId;
        protected List<SubModelType> Childrens;

        public string ModelIdentity
        {
            get => ModelId;
            set => ModelId = value;
        }

        public abstract override ItemModelType ItemModel();

        public void AddChild(SubModelType child) => Childrens.Add(child);
        public void RemoveChild(SubModelType child) => Childrens.Remove(child);
        public void RemoveChildAt(int index) => Childrens.RemoveAt(index);

        public SubModelType ChildAt(int index) => Childrens[index];
    }

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
