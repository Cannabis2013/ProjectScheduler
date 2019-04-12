using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Templates;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ProjectRelated
{
    [Serializable]
    public class HourRegistrationModel : AbstractModel<ActivityModel,HourRegistrationModel>
    {
        private readonly DateTime originalRegistrationDate;
        private string parentActivityId;
        private string activityTextContent;
        private int hours;


        public HourRegistrationModel(string identity,int hours, string userName, string text, string activityId)
        {
            ModelIdentity = identity;
            this.Hours = hours;
            this.UserName = userName;
            this.activityTextContent = text;
            parentActivityId = activityId;

            originalRegistrationDate = DateTime.Now;
            AllSubModels= new List<HourRegistrationModel>();
        }

        public string UserName { get; }

        public int Hours
        {
            get => hours;
            set => hours = value;
        }

        public DateTime OriginRegistrationDate() => originalRegistrationDate;

        public string ParentActivityId
        {
            get => parentActivityId;
            set => parentActivityId = value;
        }
        
        public string Description
        {
            get => activityTextContent;
            set => activityTextContent = value;
        }

        public string CorrespondingProjectId(ProjectManager pManager)
        {
            return pManager.ActivityModel(ParentActivityId).ParentModelIdentity();
        }

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(ModelIdentity) { Text = ModelIdentity };
            model.SubItems.Add(UserName);
            model.SubItems.Add(Hours.ToString());
            model.SubItems.Add(originalRegistrationDate.ToString("dd/MM/yyyy"));
            model.SubItems.Add(ParentModelIdentity());
            model.StateImageIndex = 0;

            return model;
        }

        public override void RemoveSubModel(string SubModelId)
        {
            throw new NotImplementedException();
        }

        public override HourRegistrationModel SubModel(string SubModelIdentity)
        {
            throw new NotImplementedException();
        }
    }
}