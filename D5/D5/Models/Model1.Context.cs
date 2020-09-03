﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace D5.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VehlutionEntities : DbContext
    {
        public VehlutionEntities()
            : base("name=VehlutionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACCESS> ACCESSes { get; set; }
        public virtual DbSet<BOOKING_DATES> BOOKING_DATES { get; set; }
        public virtual DbSet<BOOKING_FOR_POSSIBLE_PURCHASE> BOOKING_FOR_POSSIBLE_PURCHASE { get; set; }
        public virtual DbSet<BOOKING_STATUS> BOOKING_STATUS { get; set; }
        public virtual DbSet<BOOKING_TIMES> BOOKING_TIMES { get; set; }
        public virtual DbSet<CAR_BOOKING> CAR_BOOKING { get; set; }
        public virtual DbSet<CAR_BOOKING_SLOTS> CAR_BOOKING_SLOTS { get; set; }
        public virtual DbSet<CAR_DEFECT_STATUS> CAR_DEFECT_STATUS { get; set; }
        public virtual DbSet<CAR_DEFECTS> CAR_DEFECTS { get; set; }
        public virtual DbSet<CAR_PARTS> CAR_PARTS { get; set; }
        public virtual DbSet<CAR_PARTS_ORDERED> CAR_PARTS_ORDERED { get; set; }
        public virtual DbSet<CAR_STATUS> CAR_STATUS { get; set; }
        public virtual DbSet<CAR_TYPE> CAR_TYPE { get; set; }
        public virtual DbSet<CAR> CARS { get; set; }
        public virtual DbSet<CLIENT> CLIENTs { get; set; }
        public virtual DbSet<CLIENT_DOCUMENTS> CLIENT_DOCUMENTS { get; set; }
        public virtual DbSet<COLOUR> COLOURs { get; set; }
        public virtual DbSet<DEFECT_STATUS> DEFECT_STATUS { get; set; }
        public virtual DbSet<DEFECT> DEFECTS { get; set; }
        public virtual DbSet<DOCUMENT_TYPE> DOCUMENT_TYPE { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<EMPLYEE_SALES> EMPLYEE_SALES { get; set; }
        public virtual DbSet<FUEL_TYPE> FUEL_TYPE { get; set; }
        public virtual DbSet<MAKE> MAKEs { get; set; }
        public virtual DbSet<MECHANIC> MECHANICs { get; set; }
        public virtual DbSet<MECHANIC_JOB> MECHANIC_JOB { get; set; }
        public virtual DbSet<MECHANIC_TASK> MECHANIC_TASK { get; set; }
        public virtual DbSet<MODEL> MODELs { get; set; }
        public virtual DbSet<NUMBER_OF_DOORS> NUMBER_OF_DOORS { get; set; }
        public virtual DbSet<NUMBER_OF_SEATS> NUMBER_OF_SEATS { get; set; }
        public virtual DbSet<OFFER_STATUS> OFFER_STATUS { get; set; }
        public virtual DbSet<OFFER_TYPE> OFFER_TYPE { get; set; }
        public virtual DbSet<OFFER> OFFERS { get; set; }
        public virtual DbSet<ORDER> ORDERs { get; set; }
        public virtual DbSet<PART_OF_CAR> PART_OF_CAR { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<POSSIBLE_PURCHASE_STATUS> POSSIBLE_PURCHASE_STATUS { get; set; }
        public virtual DbSet<Purchase> PURCHASES { get; set; }
        public virtual DbSet<RELATIONSHIP_12> RELATIONSHIP_12 { get; set; }
        public virtual DbSet<SALE> SALES { get; set; }
        public virtual DbSet<SUPPLIER> SUPPLIERs { get; set; }
        public virtual DbSet<TASK> TASKs { get; set; }
        public virtual DbSet<TRANSMISSION> TRANSMISSIONs { get; set; }
        public virtual DbSet<USER_ROLE> USER_ROLE { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
    }
}
