﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GummiBearKingdom.Models
{
    public class EFProductRepository : IProductRepository
    {
        GummiBearDbContext db;
        public EFProductRepository()
        {
            db = new GummiBearDbContext();
        }
        public EFProductRepository(GummiBearDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Product> Products
        { get { return db.Products; } }

        public Product Save(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return product;
        }

        public void Remove(Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public void RemoveAll()
        {
            db.Database.ExecuteSqlCommand("DELETE FROM Products;");
        }
    }
}
