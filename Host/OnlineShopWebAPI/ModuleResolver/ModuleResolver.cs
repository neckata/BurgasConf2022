using System;
using System.Linq;
using System.Reflection;

namespace Host.OnlineShopWebAPI.ModuleResolver
{
    /// <summary>
    /// Provides access to Module to be used
    /// </summary>
    public class ModuleResolver : IModuleResolver
    {
        /// <summary>
        /// Creates a item command for MediatR to be used agaisnt specific Module
        /// </summary>
        /// <param name="moduleType">Module</param>
        /// <returns>Command object</returns>
        public object CreateCommand(string moduleType)
        {
            Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains(moduleType));

            Type getItemsCommand = module.GetTypes().First(x => x.Name == "GetItemsCommand");
            ConstructorInfo getItemsCommandConstructor = getItemsCommand.GetConstructor(Type.EmptyTypes);
            object getItemsCommandConstructorInstance = getItemsCommandConstructor.Invoke(new object[] { });
            return getItemsCommandConstructorInstance;

            throw new ArgumentException($"Unknown ModuleType '{moduleType}'");
        }
    }
}
