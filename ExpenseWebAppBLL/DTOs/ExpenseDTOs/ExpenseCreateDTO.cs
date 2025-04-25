using ExpenseWebAppBLL.Interfaces;

namespace ExpenseWebAppBLL.DTOs.ExpenseDTOs
{
    public class ExpenseCreateDTO : BaseDataTransferObject, IExpenseTransferObject
    {
        public int UserId { get; set; }
        public double Value { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public int CategoryId { get; set; }
    }
}
