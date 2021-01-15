using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByodLauncher.Models
{
    public class NotebookComparer
    {
        public Status ScreenRequirement { get; set; }
        //public Status Screenrequirement { get; set; }
        /*status:{
         * type:
         * message:
         *}
         */
        public Status StorageRequirement { get; set; }
        public Status RamRequirement { get; set; }
        public Status StorageTypeRequirement { get; set; }
        public Status RamTypeRequirement { get; set; }

        public NotebookComparer(Notebook notebookUser, Notebook notebookProfession)
        {
            float RamTypeProfession = float.Parse(Regex.Match(notebookProfession.RamType, @"\d+").Value);
            float RamTypeUser = float.Parse(Regex.Match(notebookUser.RamType, @"\d+").Value);
            float RamSizeProfession = float.Parse(Regex.Match(notebookProfession.RamSize, @"\d+").Value);
            float RamSizeUser = float.Parse(Regex.Match(notebookUser.RamSize, @"\d+").Value);

            ScreenRequirement = NumberComparer(float.Parse(notebookUser.ScreenSize), float.Parse(notebookProfession.ScreenSize));
            StorageRequirement = NumberComparer(float.Parse(notebookUser.StorageSize), float.Parse(notebookProfession.StorageSize));
            RamRequirement = NumberComparer(RamSizeUser, RamSizeProfession);
            StorageTypeRequirement = notebookProfession.StorageType == "SSD" ? (notebookUser.StorageType == "SSD" ? this.GoodStatus() : this.BadStatus()) : 
                (notebookUser.StorageType == "SSD" ? this.GoodStatus() : this.OkStatus("We recommend using a SSD"));
            RamTypeRequirement = NumberComparer(RamTypeUser, RamTypeProfession);
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="numberProfession">float of spec of profession</param>
        /// <param name="numberUser">flaot of spec of usersnotebook</param>
        /// <returns>good if user>proffesion, ok if 5%margin, bad else</returns>
        private Status NumberComparer(float numberProfession, float numberUser)
        {
            Status status = new Status();
            if (numberUser > numberProfession)
            {
                status.Type = "Good";
                status.Message = "";
            }else if(numberUser >= numberProfession * 0.95){
                status.Type = "Ok";
                status.Message = "Should be powerful enough. We still recommend getting a better component";
            }
            else if(numberUser < numberProfession * 0.95){
                status.Type = "Bad";
                status.Message = "";
            }
            return status;
        }

        private Status GoodStatus()
        {
            Status status = new Status();
            status.Type = "Good";
            status.Message = "";
            return status;
        }

        private Status BadStatus()
        {
            Status status = new Status();
            status.Type = "Bad";
            status.Message = "";
            return status;
        }

        private Status OkStatus(string message)
        {
            Status status = new Status();
            status.Type = "Ok";
            status.Message = message;
            return status;
        }

    }



    // not requirement
    // status: {type: success,warning,error  message:}

    public enum Requirement
    {
        bad = 1,
        ok = 2,
        good = 3,
    }
}
