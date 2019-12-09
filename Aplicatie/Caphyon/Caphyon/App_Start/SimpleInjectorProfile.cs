using Caphyon.Business.Services.System;
using Caphyon.Business.Services.System.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace Caphyon.App_Start
{
    public class SimpleInjectorProfile
    {
        private static Container _container = new Container();

        public static void Init()
        {
            // Create the container as usual.
            _container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            RegisterDependency();

           
            // This is an extension method from the integration package.
            _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            _container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));
        }

        private static void RegisterDependency()
        {

            #region Services
            _container.Register<IToDoService>(() => new ToDoService(AppContext.Create()), Lifestyle.Scoped);
          

            #endregion

            #region model
            #endregion

            #region business model
            #endregion

            #region view model
            #endregion

        }
    }
}