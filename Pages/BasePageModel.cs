using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages {

    public abstract class BasePageModel<TRepository, TDomain, TView> : PageModel
        where TRepository : ICrudMethods<TDomain>, ISorting, ISearching, IPaging {

        protected internal TRepository db;

        public string PageTitle { get; set; }
        public string PageSubTitle { get; set; }

        public int PageIndex {
            get => db.PageIndex;
            set => db.PageIndex = value;
        }
        public int TotalPages => db.TotalPages;
        public bool HasPreviousPage => db.HasPreviousPage;
        public bool HasNextPage => db.HasNextPage;

        public string SearchString {
            get => db.SearchString;
            set => db.SearchString = value;
        }

        public string SortOrder {
            get => db.SortOrder;
            set => db.SortOrder = value;
        }

        public abstract string PageLink { get; }

        protected internal BasePageModel(TRepository r) {
            db = r;
        }

        [BindProperty]
        public TView Item { get; set; }
        public IList<TView> Items { get; set; }
        public abstract string ItemId { get; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        protected internal async Task<bool> addItem() {
            if (!ModelState.IsValid) return false;
            await db.Add(toObject());

            return true;
        }

        protected internal abstract TDomain toObject();

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        protected internal async Task<bool> updateItem() {
            if (!ModelState.IsValid) return false;
            await db.Update(toObject());

            return true;
        }

        protected internal async Task getItem(string id) {
            var o = await db.Get(id);
            Item = toView(o);
        }

        protected internal abstract TView toView(TDomain domain);

        protected internal async Task deleteItem(string id) {
            await db.Delete(id);
        }

        public string SortString<T>(Expression<Func<T, object>> ex) {
            var name = GetMember.Name(ex);
            string sortOrder;
            if (string.IsNullOrEmpty(SortOrder)) sortOrder = name;
            else if (!SortOrder.StartsWith(name)) sortOrder = name;
            else if (SortOrder.EndsWith("_desc")) sortOrder = name;
            else sortOrder = name + "_desc";

            return $"{PageLink}?sortOrder={sortOrder}&searchString={SearchString}";
        }

        protected internal async Task getItems(string sortOrder,
            string currentFilter, string searchString, int? pageIndex) {

            if (searchString != null) { pageIndex = 1; }
            else { searchString = currentFilter; }

            SortOrder = sortOrder;
            SearchString = searchString;
            PageIndex = pageIndex ?? 1;
            var l = await db.Get();
            Items = new List<TView>();
            foreach (var e in l) Items.Add(toView(e));
        }

    }

}
