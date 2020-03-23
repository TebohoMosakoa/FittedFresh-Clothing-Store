using FittedFresh.DAL;
using FittedFresh.Repository;
using PagedList.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FittedFresh.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unit = new GenericUnitOfWork();
        dbMyOnlineShoppingEntities context = new dbMyOnlineShoppingEntities();
        public IPagedList<Tbl_Product> ProdcutList { get; set; }
        public HomeIndexViewModel CreateModel(string search,int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search",search??(Object)DBNull.Value)
            };
            IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ProdcutList = data
            };
        }
    }
}