using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Entities;
using TemplateS.Domain.Interfaces;
using TemplateS.Infra.Data.Context;

namespace TemplateS.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ContextCore context) : base(context) { }
    }
}
