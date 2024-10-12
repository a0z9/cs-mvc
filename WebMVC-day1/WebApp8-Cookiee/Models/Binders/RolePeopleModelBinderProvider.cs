﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp8_cookiee.Models.Binders
{
    public class RolePeopleModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
         
            IModelBinder binder = new RolePeopleModelBinder();

            return (context.Metadata.ModelType == typeof(Role))? binder : null;
        }
    }
}
