using System.Configuration;

namespace firstFormsApp_SQLServer;

public static class Helpers
{
    public static string CnnHelper(string name)
    {
        return ConfigurationManager.ConnectionStrings[name].ConnectionString;
    }
}

