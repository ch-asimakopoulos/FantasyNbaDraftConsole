namespace FantasyNbaDraftConsole
{
    using Autofac;
    
    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<App>();
            builder.RegisterType<OperationsService>().As<IOperationsService>();

            return builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            return CompositionRoot().Resolve<App>().Run(args);
        }
    }
}