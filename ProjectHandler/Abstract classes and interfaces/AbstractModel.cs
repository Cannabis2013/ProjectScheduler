using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Templates
{
    [Serializable]
    public abstract class AbstractModel <ParentModelType,SubModelType>
    {
        private ParentModelType parent;
        private List<SubModelType> SubModels = new List<SubModelType>();


        private string ModelId;

        public virtual string ModelIdentity
        {
            get => ModelId;
            set => ModelId = value;
        }

        public ParentModelType Parent
        {
            get => parent;
            set => parent = value;
        }

        public string ParentModelIdentity()
        {
            if (parent == null)
                return null;
            var property = Parent.GetType().GetProperty("ModelEntity")?.GetValue(parent,null);

            return property as string;
        }

        public abstract ListViewItem ItemModel();

        public void AddSubModel(SubModelType SubModel) => SubModels.Add(SubModel);

        public void RemoveSubModel(SubModelType SubModel) => SubModels.Remove(SubModel);
        public abstract void RemoveSubModel(string SubModelId);
        public void RemoveSubModelAt(int index) => SubModels.RemoveAt(index);

        public abstract SubModelType SubModel(string SubModelIdentity);
        public SubModelType SubModelAt(int index) => SubModels[index];

        public List<SubModelType> AllSubModels
        {
            get => SubModels;
            set => SubModels = value;
        }
    }
}