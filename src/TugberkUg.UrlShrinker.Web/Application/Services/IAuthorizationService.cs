using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugberkUg.UrlShrinker.Web.Application.Services {

    public interface IAuthorizationService {

        void  CreateUser(string userName, string password, string email);
        bool Authorize(string userName, string password);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}