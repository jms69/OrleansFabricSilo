﻿using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CalculatorService
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // The ServiceManifest.XML file defines one or more service type names.
                // RegisterServiceType maps a service type name to a .NET class.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.
                ServiceRuntime.RegisterServiceAsync("CalculatorServiceType",
                    context => new CalculatorService(context));

                ServiceEventSource.Current.ServiceTypeRegistered(
                    Process.GetCurrentProcess().Id, 
                    typeof(CalculatorService).Name);

                Thread.Sleep(Timeout.Infinite);  // Prevents this host process from terminating so services keeps running.
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
