﻿using Data_Logic_Layer;
using Data_Logic_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class BALAdminUser
    {
        private readonly DALAdminUser _dalAdminUser;

        public BALAdminUser(DALAdminUser dalAdminUser)
        {
            _dalAdminUser = dalAdminUser;
        }

        public string AddUser(User user)
        {
            return _dalAdminUser.AddUser(user);
        }
        public List<UserDetail> GetUserList()
        {
            return _dalAdminUser.GetUserList();
        }
        public string UpdateUser(int id,User user)
        {
            return _dalAdminUser.UpdateUser(id,user);
        }
        public async Task<string> DeleteUser(int userId)
        {
            return await _dalAdminUser.DeleteUser(userId);
        }
    }
}
