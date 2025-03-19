using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.DTOs;

public class CategoryDTO : BaseDataTransferObject, ICategoryTransferObject
{
    public string Name { get; set; } = null!;

    public CategoryDTO()
    {

    }
}
