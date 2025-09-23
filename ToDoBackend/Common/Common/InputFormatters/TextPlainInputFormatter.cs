using System.Net.Mime;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Common.Common.InputFormatters;

public class TextPlainInputFormatter : InputFormatter
{
    public TextPlainInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeNames.Text.Plain);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        try
        {
            using var reader = new StreamReader(context.HttpContext.Request.Body);
            var data = await reader.ReadToEndAsync();
            var model = Convert.ChangeType(data, context.ModelType);
            return await InputFormatterResult.SuccessAsync(model);
        }
        catch (Exception e)
        {
            context.ModelState.TryAddModelError("TextPlainInputFormatter", e.Message);

            return await InputFormatterResult.FailureAsync();
        }
    }

    protected override bool CanReadType(Type type)
    {
        return type == typeof(string);
    }

    public override bool CanRead(InputFormatterContext context)
    {
        return context.HttpContext.Request.ContentType == MediaTypeNames.Text.Plain;
    }
}