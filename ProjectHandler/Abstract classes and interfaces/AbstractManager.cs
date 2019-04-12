using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Templates
{
    [Serializable]
    public abstract class AbstractManager
    {
        private List<AbstractModel> ModelList = new List<AbstractModel>();

        public void AddModel(AbstractModel item) => ModelList.Add(item);

        public void RemoveModel(AbstractModel item) => ModelList?.Remove(item);
        public void RemoveModelAt(int index) => ModelList?.RemoveAt(index);

        public AbstractModel Model(AbstractModel item) => ModelList.Find(i => i.Equals(item));
        public AbstractModel Model(string id) => ModelList.Find(item => item.ModelIdentity == id);
        public AbstractModel ModelAt(int index) => ModelList[index];

        protected List<AbstractModel> Models
        {
            get => ModelList;
            set => ModelList = value;
        }

        public abstract List<string> ListModelIdentities();

        public virtual ListViewItem[] ModelListViewItem()
        {
            throw new NotImplementedException();
        }
    }

}
