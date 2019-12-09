using Caphyon.Business.Services.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caphyon.Controllers
{
    public class BaseController : Controller
    {
            #region public property
            public string CurrentUserRole
            {
                get
                {
                    return System.Web.Security.Roles.GetRolesForUser().Single();
                }
            }
            protected string GetCurrentUserId
            {
                get
                {
                    return User.Identity.Name;
                }
            }

            #endregion

            #region Protected Members
            protected IToDoService _ToDoService;
            #endregion

            #region C'tor
            public BaseController()
            {
            }

            #endregion


        
    }
}