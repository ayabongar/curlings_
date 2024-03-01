
using System.Web;


public static class ResourceString
{

    public static string GetResourceString(string resourceKey)
    {
        var globalResourceObject = HttpContext.GetGlobalResourceObject("SurveyRes", resourceKey);
        if (globalResourceObject != null)
            return globalResourceObject.ToString();
        return "Could not find description";
    }
}