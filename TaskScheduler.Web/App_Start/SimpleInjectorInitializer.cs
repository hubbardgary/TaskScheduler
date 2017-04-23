using System;
using System.Linq;
using Microsoft.Win32.TaskScheduler;
using SimpleInjector.Advanced;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Models.Recording;
using TaskScheduler.Core.Models.Shutdown;
using TaskScheduler.Core.Services;
using TaskScheduler.Core.TaskTypes.Recording;
using TaskScheduler.Core.TaskTypes.Shutdown;
using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using TaskScheduler.Web.Services.Interfaces;
using TaskScheduler.Web.Services;

[assembly: WebActivator.PostApplicationStartMethod(typeof(TaskScheduler.Web.SimpleInjectorInitializer), "Initialize")]

namespace TaskScheduler.Web
{
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>
        /// Initialize the container and register it as MVC3 Dependency Resolver.
        /// </summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.ConstructorResolutionBehavior = new LeastArgsConstructorBehavior();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<TaskService, TaskService>(Lifestyle.Scoped);
            container.Register<ITaskFolderService, TaskFolderService>(Lifestyle.Scoped);
            container.Register<ITaskTriggerBuilder, TaskTriggerBuilder>(Lifestyle.Scoped);
            container.Register<ITaskSchedulerService<RecordingTask, RecordingModel>, RecordingScheduler>(Lifestyle.Scoped);
            container.Register<ITaskSchedulerService<ShutdownTask, ShutdownModel>, ShutdownScheduler>(Lifestyle.Scoped);
            container.Register<IRecordingServices, RecordingServices>(Lifestyle.Scoped);
        }
    }

    /// <summary>
    /// Out of the box, SimpleInjector only supports classes with a single public constructor.
    /// The Microsoft.Win32.TaskScheduler library breaks this rule so we need to override this behaviour.
    /// Instead, select the constructor that requires the fewest arguments.
    /// </summary>
    public class LeastArgsConstructorBehavior : IConstructorResolutionBehavior
    {
        public ConstructorInfo GetConstructor(Type implementationType) => 
            (
            from ctor in implementationType.GetConstructors()
            orderby ctor.GetParameters().Length
            select ctor
            ).First();
    }
}