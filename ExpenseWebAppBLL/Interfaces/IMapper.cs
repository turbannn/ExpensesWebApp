﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Interfaces;

namespace ExpenseWebAppBLL.Interfaces
{
    public interface IMapper<DTO, Entity> where DTO: ITransferObject
    {
        DTO ToReadDTO(Entity entity);
        Entity ToEntity(DTO dto);
    }
}
