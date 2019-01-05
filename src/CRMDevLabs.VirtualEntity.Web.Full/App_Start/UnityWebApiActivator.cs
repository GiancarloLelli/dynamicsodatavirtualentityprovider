
using System.Web.Http;
using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CRMDevLabs.VirtualEntity.Web.Full.UnityWebApiActivator), nameof(CRMDevLabs.VirtualEntity.Web.Full.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(CRMDevLabs.VirtualEntity.Web.Full.UnityWebApiActivator), nameof(CRMDevLabs.VirtualEntity.Web.Full.UnityWebApiActivator.Shutdown))]

namespace CRMDevLabs.VirtualEntity.Web.Full
{
    public static class UnityWebApiActivator
    {
        public static void Start()
        {
            var resolver = new UnityDependencyResolver(UnityConfig.Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown() => UnityConfig.Container.Dispose();
    }
}