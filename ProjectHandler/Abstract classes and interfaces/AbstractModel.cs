using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;

namespace Templates
{
    [Serializable]
    public abstract class AbstractModel
    {
        private AbstractModel parent = null;
        private AbstractManager parentManager = null;
        private List<AbstractModel> subModels = new List<AbstractModel>();


        private string ModelId;

        public string ModelIdentity
        {
            get => ModelId;
            set => ModelId = value;
        }

        public virtual AbstractModel Parent
        {
            get => parent;
            set => parent = value;
        }

        public AbstractManager ParentManager
        {
            get => parentManager;
            set => parentManager = value;
        }

        public string ParentModelIdentity()
        {
            return parent.ModelIdentity;
        }

        public abstract ListViewItem ItemModel();

        public void AddSubModel(AbstractModel SubModel)
        {
            subModels.Add(SubModel);
            SubModel.Parent = this;
            StateChanged();
        }

        public void RemoveSubModel(AbstractModel SubModel)
        {
            subModels.Remove(SubModel);
            SubModel.Parent = null;
            StateChanged();
        }

        public void RemoveSubModel(string identity)
        {
            for (var i = 0; i < subModels.Count; i++)
            {
                var model = subModels[i];
                if (model.ModelIdentity == identity)
                {
                    subModels.RemoveAt(i);
                    StateChanged();
                    return;
                }
            }
        }

        public void RemoveSubModelAt(int index)
        {
            subModels.RemoveAt(index);
            StateChanged();
        }

        public AbstractModel SubModel(string SubModelIdentity) => 
            subModels.Find(item => item.ModelIdentity == SubModelIdentity);
        public AbstractModel SubModelAt(int index) => subModels[index];

        public List<AbstractModel> SubModels
        {
            get => subModels;
            set => subModels = value;
        }
        public List<T> AllSubModels<T>()
        {
            var result = new List<T>();
            foreach (var model in subModels)
            {
                var item = (T)Convert.ChangeType(model,typeof(T));
                result.Add(item);
            }

            return result;
        }

        public ListViewItem[] allSubItemModels() => subModels.Select(item => item.ItemModel()).ToArray();
        public void NotifyObservers()
        {
            throw new NotImplementedException();
        }

        public void StateChanged()
        {
            AbstractModel ParentProject = WarnParentObject(this);
            ParentProject.parentManager.RequestUpdate();
        }

        private AbstractModel WarnParentObject(AbstractModel model)
        {
            if (model.Parent != null)
                return WarnParentObject(model.Parent);

            return model;

        }
    }
}