using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BupMessManagement.Controllers.Admin;


namespace BupMessManagement.Models
{

    public class PaginationModel : PageModel
    {
        private readonly IConsumerOrderService _orderService;

        public PaginationModel(IConsumerOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        [BindProperty]
        public int Count { get; set; }
        [BindProperty]
        public int PageSize { get; set; } = 20;
        [BindProperty]
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        [BindProperty]
        public List<ConsumerOrder> Data { get; set; }
        public async Task OnGetAsync()
        {
            Data = await _orderService.GetPaginatedResult(CurrentPage, PageSize);
            Count = await _orderService.GetCount();
        }


    }
}
