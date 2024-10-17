using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp9_cookiee_ef.Models.Binders
{
    public class RolePeopleModelBinder : IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult? roleId = bindingContext.ValueProvider.GetValue("role");
            
            int id = (roleId is not null) ? int.Parse(roleId?.FirstValue) : 1;

            Role? role = Resources.Roles.FirstOrDefault<Role>(x=> x.Id == id);

            bindingContext.Result = ModelBindingResult.Success(role);

            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }



}
