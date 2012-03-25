using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;

namespace TugberkUg.UrlShrinker.Web.Application.Services {

    public class AuthorizationService : IAuthorizationService {

        private readonly IDocumentSession _ravenSession;

        public AuthorizationService(IDocumentStore documentStore) {

            _ravenSession = documentStore.OpenSession(
                Constants.UrlShortenerDbName
            );
        }

        public bool Authorize(string userName, string password) {

            var user = _ravenSession.Query<TugberkUg.UrlShrinker.Data.DataAccess.User>()
                .FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return false;

            return isEqual(password, user.Password);
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword) {

            throw new NotImplementedException();
        }

        public void CreateUser(string userName, string password, string email) {

            _ravenSession.Store(new TugberkUg.UrlShrinker.Data.DataAccess.User { 
                IsApproved = true,
                UserName = userName,
                Password = getHashedPassword(password),
                HashAlgorithm = "SHA512"
            });
            _ravenSession.SaveChanges();
        }

        //private helpers
        private string getHashedPassword(string rawPassword) {

            using (System.Security.Cryptography.SHA512Managed hashTool =
                new System.Security.Cryptography.SHA512Managed()) {

                Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(rawPassword);
                Byte[] EncryptedBytes = hashTool.ComputeHash(PasswordAsByte);
                hashTool.Clear();

                return Convert.ToBase64String(EncryptedBytes);
            }
        }

        private bool isEqual(string rawPassword, string hashedPasword) {

            return string.Equals(
                getHashedPassword(rawPassword), hashedPasword
            );
        }
    }
}