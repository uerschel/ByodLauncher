using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByodLauncher.Models
{
    public class NotebookComparer
    {
        public Requirement ScreenRequirement { get; set; }
        public Requirement StorageRequirement { get; set; }
        public Requirement RamRequirement { get; set; }
        public Requirement StorageTypeRequirement { get; set; }
        public Requirement RamTypeRequirement { get; set; }

        public NotebookComparer(Notebook notebookUser, Notebook notebookProfession)
        {
            ScreenRequirement = NumberComparer(float.Parse(notebookUser.ScreenSize), float.Parse(notebookProfession.ScreenSize));
            StorageRequirement = NumberComparer(float.Parse(notebookUser.StorageSize), float.Parse(notebookProfession.StorageSize));
            ScreenRequirement = NumberComparer(float.Parse(notebookUser.RamSize), float.Parse(notebookProfession.RamSize));
            StorageTypeRequirement = notebookProfession.StorageType == "SSD" ? (notebookUser.StorageType == "SSD" ? Requirement.good : Requirement.bad) : (notebookUser.StorageType == "SSD" ? Requirement.good : Requirement.ok);
            float RamTypeProfession = float.Parse(Regex.Match(notebookProfession.RamType, @"\d+").Value);
            float RamTypeUser = float.Parse(Regex.Match(notebookUser.RamType, @"\d+").Value);
            RamTypeRequirement = NumberComparer(RamTypeUser, RamTypeProfession);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="numberProfession">float of spec of profession</param>
        /// <param name="numberUser">flaot of spec of usersnotebook</param>
        /// <returns>good if user>proffesion, ok if 5%margin, bad else</returns>
        private Requirement NumberComparer(float numberProfession, float numberUser)
        {
            if (numberUser > numberProfession)
            {
                return Requirement.good;
            }
            else if (numberUser >= numberProfession * 0.95)
            {
                return Requirement.ok;
            }
            else if (numberUser <= numberProfession * 0.95)
            {
                return Requirement.bad;
            }
            return Requirement.bad;
        }
    }

    public enum Requirement
    {
        good = 3,
        ok = 2,
        bad = 1
    }
}
