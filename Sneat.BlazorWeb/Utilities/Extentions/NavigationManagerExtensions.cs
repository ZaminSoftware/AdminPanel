using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.RegularExpressions;

namespace Sneat.BlazorWeb.Utilities.Extentions;

public static class NavigationManagerExtensions
{
    public static string GetPath(this NavigationManager navigationManager)
    {
        return new StringBuilder()
            .Append(navigationManager.Uri)
            .Replace(navigationManager.BaseUri, "")
            .Insert(0, "/")
            .ToString();
    }

    public static void Reload(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(navigationManager.Uri, true);
    }

    public static void NavigateTo(this NavigationManager navigationManager, string uriPattern, List<object> parameters)
    {
        if (parameters.Count != Regex.Matches(uriPattern, "{").Count) throw new InvalidDataException();

        var stringBuilder = new StringBuilder(uriPattern);

        for (var i = 0; i < parameters.Count; i++)
        {
            var temp = stringBuilder.ToString();
            var openIndex = Regex.Match(temp, "{").Index;
            var closeIndex = Regex.Match(temp, "}").Index;
            var removeLength = closeIndex - openIndex + 1;
            var value = parameters[i].ToString() ?? string.Empty;
            stringBuilder.Remove(openIndex, removeLength).Insert(openIndex, value);
        }

        var uri = stringBuilder.ToString();

        navigationManager.NavigateTo(uri);
    }

    public static void NavigateTo(this NavigationManager navigationManager, string uriPattern, List<string> parameters, Dictionary<string, object?> queryStrings)
    {
        if (parameters.Count != Regex.Matches(uriPattern, "{").Count) throw new InvalidDataException();

        var stringBuilder = new StringBuilder(uriPattern);

        for (var i = 0; i < parameters.Count; i++)
        {
            var temp = stringBuilder.ToString();
            var openIndex = Regex.Match(temp, "{").Index;
            var closeIndex = Regex.Match(temp, "}").Index;
            var removeLength = closeIndex - openIndex + 1;
            var value = parameters[i];
            stringBuilder.Remove(openIndex, removeLength).Insert(openIndex, value);
        }

        var uri = stringBuilder.ToString();

        uri = navigationManager.GetUriWithQueryParameters(uri, queryStrings);

        navigationManager.NavigateTo(uri);
    }
}
