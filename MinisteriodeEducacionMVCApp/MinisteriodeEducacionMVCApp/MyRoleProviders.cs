using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MinisteriodeEducacionMVCApp.Models;

namespace MinisteriodeEducacionMVCApp
{
    public class MyRoleProviders : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

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

        public override string[] GetRolesForUser(string username)
        {
                ProIntBDEntities db = new ProIntBDEntities();

                var countE = db.Estudiante.Where(x => x.loginEstudiante == username).Count();
                var countPM = db.PersonalMinisterio.Where(x => x.loginMinistro == username).Count();              
                var countPC = db.Vista_PC_Rol.Where(x => x.loginPColegio == username).Count();
           
             if (countE != 0)
            {
                string idE = "R"+db.Estudiante.Where(x => x.loginEstudiante == username).FirstOrDefault().idRol;
                string[] resultS = { idE };
                return resultS;
            }
            else
            {
                if (countPM != 0)
                {
                    string idPM = "R" + db.PersonalMinisterio.Where(x => x.loginMinistro == username).FirstOrDefault().idRol;
                    string[] resultS = { idPM };
                    return resultS;
                }
                else
                {
                    if (countPC != 0)
                    {
                        string ridPC = "R" + db.Vista_PC_Rol.Where(x => x.loginPColegio == username).FirstOrDefault().idRol;
                        string[] resultS = { ridPC };
                        return resultS;
                    }
                }
            }
            string mal = "R0"; 
            string[] resultM = { mal };
            return resultM;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
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