using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ELaundry.Models;

namespace ELaundry
{
    /// <summary>Class MyRoleProvider.
    /// Implements the <see cref="System.Web.Security.RoleProvider" /></summary>
    public class MyRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets a list of the roles that a specified user is in for the configured <span class="keyword">applicationName</span>.</summary>
        /// <param name="username">The user to return a list of roles for.</param>
        /// <returns>A string array containing the names of all the roles that the specified user is in for the configured <span class="keyword">applicationName</span>.</returns>
        public override string[] GetRolesForUser(string username)
        {
            using (ELaundryDBEntities objContext = new ELaundryDBEntities())
            {
                var objUser = objContext.tblUsers.FirstOrDefault(x => x.Username == username);
                if (objUser == null)
                {
                    return null;
                }
                else
                {
                    string[] ret = objUser.tblUserRoles.Select(x => x.tblRole.RoleName).ToArray();
                    return ret;
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}