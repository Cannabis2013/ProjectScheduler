﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Templates;

namespace ProjectRelated
{
    [Serializable]
    public class ProjectModel : AbstractModel
    {
        private string pLeaderId, shortDescription;
        private DateTime startDate, endDate;

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public string projectLeaderId
        {
            get => pLeaderId;
            set => pLeaderId = value;
        }

        public string ShortDescription
        {
            get => shortDescription;
            set => shortDescription = value;
        }

        public ListViewItem[] ActivityItemModels()
        {
            int count = AllSubModels<ActivityModel>().Count, index = 0;
            var models = new ListViewItem[count];
            foreach (var a in AllSubModels<ActivityModel>())
            {
                models[index] = a.ItemModel();
                index++;
            }

            return models;
        }
        public List<ActivityModel> Activities(string userName)
        {
            var userActivities = AllSubModels<ActivityModel>().Where(item => item.IsUserAssigned(userName));
            return userActivities.Select(item => new ActivityModel(item)).ToList();
        }

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(ModelIdentity);
            
            model.SubItems.Add(projectLeaderId);

            model.SubItems.Add(StartDate.ToString("dd/MM/yyyy"));
            model.SubItems.Add(EndDate.ToString("dd/MM/yyyy"));

            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }
    }
}