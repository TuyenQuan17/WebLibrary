using GaziLibrary.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GaziLibrary.DataAccess.Abstract
{
    public interface IAuthorDal : IEntityRepository<Author>
    {
        
    }
}
