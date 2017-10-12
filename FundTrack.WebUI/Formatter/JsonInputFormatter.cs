using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.WebUI.Formatter
{
    public class JsonInputFormatter : TextInputFormatter
    {
        public JsonInputFormatter()
        {

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add("application/json");
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var request = context.HttpContext.Request;

            using (var streamReader = context.ReaderFactory(request.Body, encoding))
            {
                var type = context.ModelType;

                if (type == typeof(ImportDetailPrivatViewModel))

                {
                    var i = 10;
                }
                try
                {
                    var serializer = new JsonSerializer();
                    var model = serializer.Deserialize(streamReader, type);
                    return InputFormatterResult.SuccessAsync(model);
                }
                catch (Exception)
                {
                    return InputFormatterResult.FailureAsync();
                }
            }
        }
    }
}

