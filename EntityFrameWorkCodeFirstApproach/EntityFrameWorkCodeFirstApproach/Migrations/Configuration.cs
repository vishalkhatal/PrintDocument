namespace EntityFrameWorkCodeFirstApproach.Migrations
{
    using EntityFrameWorkCodeFirstApproach.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameWorkCodeFirstApproach.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "EntityFrameWorkCodeFirstApproach.Models.ApplicationDbContext";
        }

        protected override void Seed(EntityFrameWorkCodeFirstApproach.Models.ApplicationDbContext context)
        {
            var dept = new List<Department>
            {
            new Department{DepartmentID=1,DepartmentName="Alexander"}
                 };

            dept.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();
        }
    }
}
