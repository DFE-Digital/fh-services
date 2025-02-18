﻿using System.Text.Encodings.Web;
using System.Text;

namespace FamilyHubs.ServiceDirectory.Core.UrlHelpers;

public static class DictionaryForQueryParamsExtension
{
    public static Dictionary<string, string?> AddOptionalQueryParams(this Dictionary<string, string?> queryParams, string key, object? value)
    {
        if (value != null)
        {
            queryParams.Add(key, value.ToString());
        }

        return queryParams;
    }

    public static Dictionary<string, string?> AddOptionalQueryParams(this Dictionary<string, string?> queryParams, string key, IEnumerable<string>? values)
    {
        if (values is not null)
        {
            var joined = string.Join(',', values);
            if (joined != string.Empty)
            {
                queryParams.Add(key, joined);
            }
        }

        return queryParams;
    }

    public static string CreateUriWithQueryString(this IEnumerable<KeyValuePair<string, string?>> queryString, string uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        ArgumentNullException.ThrowIfNull(queryString);

        var anchorIndex = uri.IndexOf('#');
        var uriToBeAppended = uri;
        var anchorText = "";
        // If there is an anchor, then the query string must be inserted before its first occurrence.
        if (anchorIndex != -1)
        {
            anchorText = uri.Substring(anchorIndex);
            uriToBeAppended = uri.Substring(0, anchorIndex);
        }

        var queryIndex = uriToBeAppended.IndexOf('?');
        var hasQuery = queryIndex != -1;

        var sb = new StringBuilder();
        sb.Append(uriToBeAppended);
        foreach (var parameter in queryString)
        {
            if (parameter.Value == null)
            {
                continue;
            }

            sb.Append(hasQuery ? '&' : '?');
            sb.Append(UrlEncoder.Default.Encode(parameter.Key));
            sb.Append('=');
            sb.Append(UrlEncoder.Default.Encode(parameter.Value));
            hasQuery = true;
        }

        sb.Append(anchorText);
        return sb.ToString();
    }
}