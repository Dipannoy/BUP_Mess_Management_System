using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2.Models
{
    public enum APIResultStatus
    {
        success,
        error,
        warning
    }
    public class APIResultReturnObject
    {
        public APIResultReturnObject()
        {
            ErrorList = new List<APIResultError>();
            WarningList = new List<APIResultWarning>();
        }
        public APIResultStatus Status { get; set; }
        public List<APIResultError> ErrorList { get; set; }
        public List<APIResultWarning> WarningList { get; set; }
        public object Data { get; set; }

        public bool HasError
        {
            get
            {
                return (ErrorList.Count == 0) ? false : true;
            }
        }
        public bool HasWarning
        {
            get
            {
                return (WarningList.Count == 0) ? false : true;
            }
        }
    }
    public class APIResultError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
    public class APIResultWarning
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
