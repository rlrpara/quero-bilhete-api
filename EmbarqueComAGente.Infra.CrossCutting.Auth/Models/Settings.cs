using System;

namespace EmbarqueComAGente.Infra.CrossCutting.Auth.Models
{
    public static class Settings
    {
        public static string Secret = Environment.GetEnvironmentVariable("Secret");
    }
}
