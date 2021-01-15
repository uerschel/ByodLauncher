using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByodLauncher.Models
{
    public class NotebookComparer
    {
        public List<Status> Statuses { get; set; }

        public NotebookComparer(Notebook notebookUser, Notebook notebookProfession)
        {
            Statuses = new List<Status>();
            float RamTypeProfession = float.Parse(Regex.Match(notebookProfession.RamType, @"\d+").Value);
            float RamTypeUser = float.Parse(Regex.Match(notebookUser.RamType, @"\d+").Value);
            float RamSizeProfession = float.Parse(Regex.Match(notebookProfession.RamSize, @"\d+").Value);
            float RamSizeUser = float.Parse(Regex.Match(notebookUser.RamSize, @"\d+").Value);

            Statuses.Add(NumberComparer(float.Parse(notebookUser.ScreenSize), float.Parse(notebookProfession.ScreenSize), "Screen size", notebookUser.ScreenSize));
            Statuses.Add(NumberComparer(float.Parse(notebookUser.StorageSize), float.Parse(notebookProfession.StorageSize), "Storage size", notebookUser.StorageSize));
            Statuses.Add(NumberComparer(RamSizeUser, RamSizeProfession, "Ram size", notebookUser.RamSize));
            Statuses.Add(NumberComparer(RamTypeUser, RamTypeProfession, "Ram type", notebookUser.RamType));
            Statuses.Add(notebookProfession.StorageType.Contains("SSD") ? (notebookUser.StorageType.Contains("SSD") ? this.GetStatus("This is suitable for your profession", "Storage type", notebookUser.StorageType, "success") : this.GetStatus("You should reconsider your option", "Storage type", notebookUser.StorageType, "error")) :
                notebookUser.StorageType.Contains("SSD") ? this.GetStatus("This is suitable for your profession", "Storage type", notebookUser.StorageType, "success") : this.GetStatus("This should be enough for you, we recommend a SSD", "Storage type", notebookUser.StorageType, "warning"));
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="numberProfession">float of spec of profession</param>
        /// <param name="numberUser">flaot of spec of usersnotebook</param>
        /// <returns>good if user>proffesion, ok if 5%margin, bad else</returns>
        private Status NumberComparer(float numberUser, float numberProfession, string name, string specs)
        {
            Status status = new Status();
            if (numberUser >= numberProfession)
            {
                status.Type = "success";
                status.Message = "This is suitable for your profession";
            }
            else if (numberUser >= numberProfession * 0.95)
            {
                status.Type = "warning";
                status.Message = "Should be good enough. We still recommend getting a better component";
            }
            else if (numberUser < numberProfession * 0.95)
            {
                status.Type = "error";
                status.Message = "You should reconsider your option";
            }
            status.Name = name;
            status.Specs = specs;
            return status;
        }
        private Status GetStatus(string message, string name, string specs, string type)
        {
            Status status = new Status();
            status.Type = type;
            status.Message = message;
            status.Name = name;
            status.Specs = specs;
            return status;
        }

    }

    public enum Requirement
    {
        bad = 1,
        ok = 2,
        good = 3,
    }
}
