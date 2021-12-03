using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace YoumaconSecurityOps.Web.Client.Shared
{
    public partial class FooterNav: ComponentBase
    {
        [Parameter] public EventCallback<string> ThemeColorChanged { get; set; }
        
        private static string AssemblyProductVersion
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
                return attributes.Length == 0 ?
                    "" :
                    ((AssemblyInformationalVersionAttribute)attributes[0]).InformationalVersion;
            }
        }

        private static string ApplicationDevelopmentCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ?
                    "" :
                    ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}
