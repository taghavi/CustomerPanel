using Bko.Business;
using Bko.Service.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Bko.Service.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(Microsoft.Owin.Security.OAuth.OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);


                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                else
                {
                    var res = new ReportBlo().GetName_Bid_Balance(user.UserName);
                    if (res == null)
                    {
                        context.SetError("عدم ثبت مشتری", "مشتری با این مشخصات هنوز ثبت نشده است.");
                        return;
                    }

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    identity.AddClaim(new Claim("role", "user"));

                    identity.AddClaim(new Claim(ClaimTypes.GivenName, res.CompanyName));
                    identity.AddClaim(new Claim(ClaimTypes.Sid, res.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.UserData, res.BalanceAmount.ToString()));
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {
                                "companyName", identity.FindFirst(ClaimTypes.GivenName).Value
                            },
                            {
                                "credit", identity.FindFirst(ClaimTypes.UserData).Value
                            },
                        });

                    context.Validated(new AuthenticationTicket(identity, props));
                }
            }



        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}