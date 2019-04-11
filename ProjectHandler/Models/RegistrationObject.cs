using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Templates;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ProjectRelated
{
    [Serializable]
    public class RegistrationObject : ModelEntity<ListViewItem>
    {
        private readonly DateTime originalRegistrationDate;
        private string regId;
        private string parentActivityId;
        private string userName;
        private string activityTextContent;

        public int Hours { get; set; }

        public RegistrationObject(string title,int hours, string userName, string text, string activityId)
        {
            regId = title;
            this.Hours = hours;
            this.userName = userName;
            this.activityTextContent = text;
            parentActivityId = activityId;

            originalRegistrationDate = DateTime.Now;
        }

        public string RegistrationId
        {
            get => regId;
            set => regId = value;
        }

        public DateTime OriginRegistrationDate() => originalRegistrationDate;

        public string ParentActivityId
        {
            get => parentActivityId;
            set => parentActivityId = value;
        }

        public string UserName
        {
            get => userName;
            set => userName = value;
        }

        public string Description
        {
            get => activityTextContent;
            set => activityTextContent = value;
        }

        public string CorrespondingProjectId(ProjectManager pManager)
        {
            return pManager.Activity(ParentActivityId).ParentProjectId;
        }

        public override ListViewItem ItemModel(ListMode mode = ListMode.Tile)
        {
            if (mode == ListMode.List)
                return ListListViewModel();
            else
                return TileListViewModel();
        }

        private ListViewItem ListListViewModel()
        {
            var model = new ListViewItem(RegistrationId) {Text = regId};
            model.SubItems.Add(userName);
            model.SubItems.Add(Hours.ToString());
            model.SubItems.Add(originalRegistrationDate.ToString("dd/MM/yyyy"));
            model.SubItems.Add(parentActivityId);
            model.StateImageIndex = 0;

            return model;
        }

        private ListViewItem TileListViewModel()
        {
            var model = new ListViewItem(RegistrationId);

            var userId = new StringBuilder("User: ");
            userId.Append(UserName);
            model.Text = userId.ToString();

            var hourString = new StringBuilder("Registered hours: ");
            hourString.Append(Hours.ToString());
            model.SubItems.Add(hourString.ToString());

            model.SubItems.Add(Hours.ToString());

            var origWeek = new StringBuilder("Original registered week: ");
            origWeek.Append(originalRegistrationDate.ToString(CultureInfo.InvariantCulture));

            model.SubItems.Add(origWeek.ToString());

            model.StateImageIndex = 0;

            return model;
        }
    }
}