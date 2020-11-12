using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mess_Management_System_Alpha_V2
{
    public class AzureAdB2COptions
    {
        public const string PolicyAuthenticationProperty = "Policy";

        public AzureAdB2COptions()
        {
            AzureAdB2CInstance = "https://wpbackup.bup.edu.bd/";
        }

        public string ClientId { get; set; }
        public string AzureAdB2CInstance { get; set; }
        public string Tenant { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string SignInPolicyId { get; set; }
        public string SignUpPolicyId { get; set; }
        public string ResetPasswordPolicyId { get; set; }
        public string EditProfilePolicyId { get; set; }
        public string RedirectUri { get; set; }

        public string DefaultPolicy => SignUpSignInPolicyId;
        // public string Authority => $"{AzureAdB2CInstance}/{Tenant}/{DefaultPolicy}/v2.0";
        public string Authority { get; set; } // => "https://wpbackup.bup.edu.bd/";
        public string ClientSecret { get; set; }
        public string ApiUrl { get; set; }
        public string ApiScopes { get; set; }
    }
}
