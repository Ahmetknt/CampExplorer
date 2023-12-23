using CampExplorer.Web.Models;
using Core.Dtos;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampExplorer.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> Register(RegisterInput registerInput);
        Task<Response<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}