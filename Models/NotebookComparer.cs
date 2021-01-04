using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByodLauncher.Models
{
    public class NotebookComparer
    {
        public Requirement ScreenRequirement { get; set; }
        public Requirement StorageRequirement { get; set; }
        public Requirement RamRequirement { get; set; }

        public NotebookComparer(Notebook notebookUser, Notebook notebookProfession)
        {
            if (float.Parse(notebookProfession.ScreenSize) <= float.Parse(notebookUser.ScreenSize))
            {
                ScreenRequirement = Requirement.good;
            }
            else
            {
                ScreenRequirement = Requirement.bad;
            }
            if (float.Parse(notebookProfession.StorageSize) <= float.Parse(notebookUser.StorageSize))
            {
                StorageRequirement = Requirement.good;
            }
            else
            {
                StorageRequirement = Requirement.bad;
            }
            if (float.Parse(notebookProfession.RamSize) <= float.Parse(notebookUser.RamSize))
            {
                RamRequirement = Requirement.good;
            }
            else
            {
                RamRequirement = Requirement.bad;
            }
        }
    }

    public enum Requirement
    {
        good = 1,
        ok = 2,
        bad = 3
    }
}
