using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mess_Management_System_Alpha_V2.Data;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Mess_Management_System_Alpha_V2.Controllers.SessionChecker;

//using System.Web.Script.Serialization;


using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace BupMessManagement
{
    public static class Session
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
       
    }
}
