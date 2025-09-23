using Common.Common.Binders;
using Common.Common.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Common.Common.BinderProviders;

public class FormDataJsonBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (context.Metadata.ModelType == typeof(IInternalModelDto))
            return new BinderTypeModelBinder(typeof(FormDataJsonBinder));

        return null;
    }
}