using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Quantity {

    public class UnitsPage : BasePage<IUnitsRepository, Unit, UnitView, UnitData> {

        protected internal UnitsPage(IUnitsRepository r) : base(r) {
            PageTitle = "Units";
        }
        public IEnumerable<SelectListItem> Measures { get; }

        public override string ItemId => Item.Id;

        protected internal override Unit toObject(UnitView view) {
            return UnitViewFactory.Create(view);
        }

        protected internal override UnitView toView(Unit obj) {
            return UnitViewFactory.Create(obj);
        }
        public string GetMeasureName (string measureId)
        {
            foreach (var m in Measures)
                if (m.Value == measureId)
                    return m.Text;
            return "Unspecified";
        }
    }

}
