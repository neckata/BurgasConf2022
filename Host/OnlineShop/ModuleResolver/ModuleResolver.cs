using OnlineShop.Shared.Infrastructure.Enums;
using System;
using System.Linq;
using System.Reflection;

namespace Host.OnlineShop.ModuleResolver
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
        /// <param name="request">Data</param>
        /// <param name="itemsType">Type of item</param>
        /// <returns>Command object</returns>
        public object CreateCommand(string moduleType, object request, ItemsTypeEnum itemsType)
        {
            Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains(moduleType));
            switch (itemsType)
            {
                case ItemsTypeEnum.View:
                    {
                        Type getItemsCommand = module.GetTypes().First(x => x.Name == "GetItemsCommand");
                        ConstructorInfo getItemsCommandConstructor = getItemsCommand.GetConstructor(Type.EmptyTypes);
                        object getItemsCommandConstructorInstance = getItemsCommandConstructor.Invoke(new object[] { });
                        return getItemsCommandConstructorInstance;
                    }
            }

            throw new ArgumentException($"Unknown ModuleType '{moduleType}'");
        }
    }
}
