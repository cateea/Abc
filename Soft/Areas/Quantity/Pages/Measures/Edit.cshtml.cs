﻿using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.Measures {

    public class EditModel : MeasuresPage {

        public EditModel(IMeasureRepository r) : base(r) { }


        public async Task<IActionResult> OnGetAsync(string id) {
            await getObject(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            await updateObject();

            return RedirectToPage("./Index");
        }

    }

}
