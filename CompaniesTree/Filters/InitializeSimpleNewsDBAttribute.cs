using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using CompaniesTree.DAL;

namespace CompaniesTree.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleCompaniesDBAttribute : ActionFilterAttribute
    {
        private static CompaniesDBInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class CompaniesDBInitializer
        {
            public CompaniesDBInitializer()
            {
                Database.SetInitializer<CompaniesDAL>(null);

                try
                {
                    using (var dal = new CompaniesDAL())
                    {
                        if (!dal.Database.Exists())
                        {
                            ((IObjectContextAdapter)dal).ObjectContext.CreateDatabase();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The companies database could not be initialized.", ex);
                }
            }
        }
    }
}
