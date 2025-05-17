using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Taller.src.requesthelpers;

using Microsoft.Net.Http.Headers;

namespace Taller.src.extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationMetaData metadata)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            response.Headers.Append("Pagination", JsonSerializer.Serialize(metadata, options));
            response.Headers.Append(HeaderNames.AccessControlExposeHeaders, "Pagination");

        }
    }
}