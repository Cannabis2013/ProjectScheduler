using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;

namespace Templates
{
    [Serializable]
    public abstract class AbstractManager
    {
        private List<AbstractModel> ModelList = new List<AbstractModel>();
        
        public void AddModel(AbstractModel item)
        {
            ModelList.Add(item);
            item.ParentManager = this;
            RequestUpdate();
        }

        public void RemoveModel(string identity)
        {
            for (var i = 0; i < ModelList.Count; i++)
            {
                var model = ModelList[i];
                if (model.ModelIdentity == identity)
                {
                    ModelList.RemoveAt(i);
                    return;
                }
            }
        }
        public void RemoveModelAt(int index) => ModelList?.RemoveAt(index);

        public AbstractModel Model(AbstractModel item) => ModelList.Find(i => i.Equals(item));
        public AbstractModel Model(string id) => ModelList.Find(item => item.ModelIdentity == id);
        public AbstractModel ModelAt(int index) => ModelList[index];

        protected List<AbstractModel> Models
        {
            get => ModelList;
            set => ModelList = value;
        }

        protected List<T> AllModels<T>() => 
            ModelList.Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();

        public abstract List<string> ListModelIdentities();

        public virtual ListViewItem[] ModelListViewItem()
        {
            throw new NotImplementedException();
        }

        public abstract void RequestUpdate();
    }

}
