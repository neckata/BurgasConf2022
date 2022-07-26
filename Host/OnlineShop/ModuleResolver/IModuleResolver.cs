﻿using OnlineShop.Shared.Infrastructure.Enums;

namespace Host.OnlineShop.ModuleResolver
{
    /// <summary>
    /// Provides access to Module to be used
    /// </summary>
    public interface IModuleResolver
    {
        /// <summary>
        /// Creates a item command for MediatR to be used agaisnt specific Module
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="itemsType"></param>
        /// <returns>Command object</returns>
        public object CreateCommand(string moduleType, ItemsTypeEnum itemsType);
    }
}
