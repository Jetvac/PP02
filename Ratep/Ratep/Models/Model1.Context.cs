﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ratep.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Practik1282Entities : DbContext
    {
        public Practik1282Entities()
            : base("name=Practik1282Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Manufactory> Manufactory { get; set; }
        public virtual DbSet<ManufactoryType> ManufactoryType { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderPosition> OrderPosition { get; set; }
        public virtual DbSet<Part_assembly_unit> Part_assembly_unit { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Structure> Structure { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Card_work> Card_work { get; set; }
        public virtual DbSet<MaterialCard> MaterialCard { get; set; }
        public virtual DbSet<FA_Card_work> FA_Card_work { get; set; }
        public virtual DbSet<FA_Employees> FA_Employees { get; set; }
        public virtual DbSet<FA_Material> FA_Material { get; set; }
        public virtual DbSet<FA_Material_card> FA_Material_card { get; set; }
        public virtual DbSet<FA_Nomencloture> FA_Nomencloture { get; set; }
        public virtual DbSet<FA_Operation> FA_Operation { get; set; }
        public virtual DbSet<FA_Post> FA_Post { get; set; }
        public virtual DbSet<FA_Structure> FA_Structure { get; set; }
        public virtual DbSet<FA_Unit> FA_Unit { get; set; }
    
        [DbFunction("Practik1282Entities", "GetConstruction")]
        public virtual IQueryable<GetConstruction_Result> GetConstruction(Nullable<int> num_product, Nullable<int> quantity)
        {
            var num_productParameter = num_product.HasValue ?
                new ObjectParameter("Num_product", num_product) :
                new ObjectParameter("Num_product", typeof(int));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("Quantity", quantity) :
                new ObjectParameter("Quantity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetConstruction_Result>("[Practik1282Entities].[GetConstruction](@Num_product, @Quantity)", num_productParameter, quantityParameter);
        }
    
        [DbFunction("Practik1282Entities", "GetReverseConstruction")]
        public virtual IQueryable<GetReverseConstruction_Result> GetReverseConstruction(Nullable<int> num_product)
        {
            var num_productParameter = num_product.HasValue ?
                new ObjectParameter("Num_product", num_product) :
                new ObjectParameter("Num_product", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetReverseConstruction_Result>("[Practik1282Entities].[GetReverseConstruction](@Num_product)", num_productParameter);
        }
    
        public virtual int DT_Card_work(Nullable<int> num_product)
        {
            var num_productParameter = num_product.HasValue ?
                new ObjectParameter("Num_product", num_product) :
                new ObjectParameter("Num_product", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Card_work", num_productParameter);
        }
    
        public virtual int DT_Employees(Nullable<int> code_empl)
        {
            var code_emplParameter = code_empl.HasValue ?
                new ObjectParameter("Code_empl", code_empl) :
                new ObjectParameter("Code_empl", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Employees", code_emplParameter);
        }
    
        public virtual int DT_Material(Nullable<int> num_material)
        {
            var num_materialParameter = num_material.HasValue ?
                new ObjectParameter("Num_material", num_material) :
                new ObjectParameter("Num_material", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Material", num_materialParameter);
        }
    
        public virtual int DT_Nomencloture(Nullable<int> num_product)
        {
            var num_productParameter = num_product.HasValue ?
                new ObjectParameter("Num_product", num_product) :
                new ObjectParameter("Num_product", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Nomencloture", num_productParameter);
        }
    
        public virtual int DT_Operation(Nullable<int> num_operation)
        {
            var num_operationParameter = num_operation.HasValue ?
                new ObjectParameter("Num_operation", num_operation) :
                new ObjectParameter("Num_operation", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Operation", num_operationParameter);
        }
    
        public virtual int DT_Passport_data(string serial, string number)
        {
            var serialParameter = serial != null ?
                new ObjectParameter("Serial", serial) :
                new ObjectParameter("Serial", typeof(string));
    
            var numberParameter = number != null ?
                new ObjectParameter("Number", number) :
                new ObjectParameter("Number", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Passport_data", serialParameter, numberParameter);
        }
    
        public virtual int DT_Post(Nullable<int> code_post)
        {
            var code_postParameter = code_post.HasValue ?
                new ObjectParameter("Code_post", code_post) :
                new ObjectParameter("Code_post", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Post", code_postParameter);
        }
    
        public virtual int DT_Structure(Nullable<int> num_product_where, Nullable<int> num_product_what)
        {
            var num_product_whereParameter = num_product_where.HasValue ?
                new ObjectParameter("Num_product_where", num_product_where) :
                new ObjectParameter("Num_product_where", typeof(int));
    
            var num_product_whatParameter = num_product_what.HasValue ?
                new ObjectParameter("Num_product_what", num_product_what) :
                new ObjectParameter("Num_product_what", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Structure", num_product_whereParameter, num_product_whatParameter);
        }
    
        public virtual int DT_Unit(Nullable<int> code_unit)
        {
            var code_unitParameter = code_unit.HasValue ?
                new ObjectParameter("Code_unit", code_unit) :
                new ObjectParameter("Code_unit", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DT_Unit", code_unitParameter);
        }
    
        public virtual ObjectResult<GetAvailableItems_Result> GetAvailableItems(Nullable<int> forItemID)
        {
            var forItemIDParameter = forItemID.HasValue ?
                new ObjectParameter("ForItemID", forItemID) :
                new ObjectParameter("ForItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAvailableItems_Result>("GetAvailableItems", forItemIDParameter);
        }
    
        public virtual int IT_Material_card(Nullable<int> num_product, Nullable<int> num_material, Nullable<int> quantity)
        {
            var num_productParameter = num_product.HasValue ?
                new ObjectParameter("Num_product", num_product) :
                new ObjectParameter("Num_product", typeof(int));
    
            var num_materialParameter = num_material.HasValue ?
                new ObjectParameter("Num_material", num_material) :
                new ObjectParameter("Num_material", typeof(int));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("Quantity", quantity) :
                new ObjectParameter("Quantity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IT_Material_card", num_productParameter, num_materialParameter, quantityParameter);
        }
    
        public virtual int IT_Nomencloture(string name_product)
        {
            var name_productParameter = name_product != null ?
                new ObjectParameter("Name_product", name_product) :
                new ObjectParameter("Name_product", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IT_Nomencloture", name_productParameter);
        }
    
        public virtual int IT_Structure(Nullable<int> name_product_where, Nullable<int> name_product_what, Nullable<int> quantity_product)
        {
            var name_product_whereParameter = name_product_where.HasValue ?
                new ObjectParameter("Name_product_where", name_product_where) :
                new ObjectParameter("Name_product_where", typeof(int));
    
            var name_product_whatParameter = name_product_what.HasValue ?
                new ObjectParameter("Name_product_what", name_product_what) :
                new ObjectParameter("Name_product_what", typeof(int));
    
            var quantity_productParameter = quantity_product.HasValue ?
                new ObjectParameter("Quantity_product", quantity_product) :
                new ObjectParameter("Quantity_product", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IT_Structure", name_product_whereParameter, name_product_whatParameter, quantity_productParameter);
        }
    
        public virtual int NodeAmmount(Nullable<int> num_product_where, ObjectParameter result)
        {
            var num_product_whereParameter = num_product_where.HasValue ?
                new ObjectParameter("Num_product_where", num_product_where) :
                new ObjectParameter("Num_product_where", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("NodeAmmount", num_product_whereParameter, result);
        }
    
        public virtual int UT_Nomencloture(Nullable<int> num_product, string name_product)
        {
            var num_productParameter = num_product.HasValue ?
                new ObjectParameter("Num_product", num_product) :
                new ObjectParameter("Num_product", typeof(int));
    
            var name_productParameter = name_product != null ?
                new ObjectParameter("Name_product", name_product) :
                new ObjectParameter("Name_product", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UT_Nomencloture", num_productParameter, name_productParameter);
        }
    
        public virtual int UT_Structure(Nullable<int> num_product_where, Nullable<int> num_product_what, Nullable<int> quantity)
        {
            var num_product_whereParameter = num_product_where.HasValue ?
                new ObjectParameter("Num_product_where", num_product_where) :
                new ObjectParameter("Num_product_where", typeof(int));
    
            var num_product_whatParameter = num_product_what.HasValue ?
                new ObjectParameter("Num_product_what", num_product_what) :
                new ObjectParameter("Num_product_what", typeof(int));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("Quantity", quantity) :
                new ObjectParameter("Quantity", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UT_Structure", num_product_whereParameter, num_product_whatParameter, quantityParameter);
        }
    }
}