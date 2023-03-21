using GaziLibrary.DataAccess.Abstract;
using GaziLibrary.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GaziLibrary.DataAccess.Concrete
{
    public class EfAuthorDal : EfEntityRepositoryBase<Author, OracleContext>, IAuthorDal
    {
        
    }
}
