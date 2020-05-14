using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Core.ViewModels;

namespace WebCrawler.AppHelpers
{
    public static class SessionHelper
    {
        private const string Key = "CurrentSession";
        public static void SetSession(this ISession session, object value)
        {

            ApplicationHelper.SetObject(session, Key, value);
        }


        public static SessionModel GetSession(this ISession session)
        {
            var currentSession = ApplicationHelper.GetObject<SessionModel>(session, Key);
            return currentSession;
        }





        public static void KillSession(this ISession session)
        {
            ApplicationHelper.RemoveObject(session, Key);
        }


    }
}
