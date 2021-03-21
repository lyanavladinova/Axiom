using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.Roles
{
    public class RoleAddViewModel
    {
        public List<RoleDTO> Roles { get; set; }

        public List<UserDTO> Users { get; set; }
    }
}
