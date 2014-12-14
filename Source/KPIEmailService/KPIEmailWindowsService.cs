using System;
using System.ServiceProcess;
using System.ServiceModel;
using System.Threading;
using System.Configuration;
using System.Net;
using System.ServiceModel.Description;
using System.Globalization;

namespace KPIEmailService
{
    class KPIEmailWindowsService : ServiceBase
    {
        public ServiceHost contentSharingHost = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="KPIEmailWindowsService"/> class.
        /// </summary>
        public KPIEmailWindowsService()
        {
            ServiceName = "KPIEmailWindowsService";
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main()
        {
            KPIEmailWindowsService service = new KPIEmailWindowsService();
            
#if DEBUG
            service.OnDebugMode_Start();
            Console.WriteLine("Service Started...");
            System.Threading.Thread.Sleep(Timeout.Infinite);
            service.OnDebugMode_Stop();
#else

            ServiceBase.Run(service);
#endif
        }

        public void OnDebugMode_Start()
        {
            OnStart(null);
        }
        public void OnDebugMode_Stop()
        {
            OnStop();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is 
        /// sent to the service by the Service Control Manager (SCM) or when the 
        /// operating system starts (for a service that starts automatically). 
        /// Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            JobScheduler.Start();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop 
        /// command is sent to the service by the Service Control Manager (SCM). 
        /// Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
        }
    }
}
