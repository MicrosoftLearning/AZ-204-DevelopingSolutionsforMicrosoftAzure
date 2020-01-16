using AdventureWorks.Context;
using AdventureWorks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Pages
{
    public class Index : PageModel
    {
        private readonly IAdventureWorksProductContext _productContext;

        public Index(IAdventureWorksProductContext productContext)
        {
            _productContext = productContext;
        }

        [BindProperty(SupportsGet = true)]
        public List<Model> Models { get; set; } = new List<Model>();

        public async Task OnGetAsync()
        {
            this.Models = await _productContext.GetModelsAsync();
        }
    }
}