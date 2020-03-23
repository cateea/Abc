﻿using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.Measures {

    public class DeleteModel : MeasuresPage {

        public DeleteModel(IMeasureRepository r) : base(r) { }

        public async Task<IActionResult> OnGetAsync(string id, string fixedFilter, string fixedValue) 
        {
            await getObject(id);
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string fixedFilter, string fixedValue)
        {
            await deleteObject(id);
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            return Redirect($"/Quantity/Measures/Index?fixedFilter={FixedFilter}&fixedValue={fixedValue}");
        }

    }

}
