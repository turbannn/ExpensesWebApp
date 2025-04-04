using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.CategoryDTOs;

public class CategoryDTO : BaseDataTransferObject, ICategoryTransferObject
{
    public string Name { get; set; } = null!;

    public CategoryDTO()
    {

    }
}
