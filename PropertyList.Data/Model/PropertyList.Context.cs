﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyList.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PropertyListing_DevEntities : DbContext
    {
        public PropertyListing_DevEntities()
            : base("name=PropertyListing_DevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<utProperty> utProperties { get; set; }
        public virtual DbSet<utStaff> utStaffs { get; set; }
        public virtual DbSet<utStaffRole> utStaffRoles { get; set; }
    
        public virtual int usp_InsertSingleStaff(string firstName, string lastName, string email, string password, Nullable<int> role, Nullable<System.DateTime> createdDate, Nullable<System.DateTime> updatedDate)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("firstName", firstName) :
                new ObjectParameter("firstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("lastName", lastName) :
                new ObjectParameter("lastName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var roleParameter = role.HasValue ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(int));
    
            var createdDateParameter = createdDate.HasValue ?
                new ObjectParameter("createdDate", createdDate) :
                new ObjectParameter("createdDate", typeof(System.DateTime));
    
            var updatedDateParameter = updatedDate.HasValue ?
                new ObjectParameter("updatedDate", updatedDate) :
                new ObjectParameter("updatedDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_InsertSingleStaff", firstNameParameter, lastNameParameter, emailParameter, passwordParameter, roleParameter, createdDateParameter, updatedDateParameter);
        }
    
        public virtual int usp_ValidateStaffLogin(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_ValidateStaffLogin", emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<usp_CheckStaffAccount_Result> usp_CheckStaffAccount(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_CheckStaffAccount_Result>("usp_CheckStaffAccount", emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<usp_GetPropertyById_Result> usp_GetPropertyById(Nullable<int> propertyId)
        {
            var propertyIdParameter = propertyId.HasValue ?
                new ObjectParameter("propertyId", propertyId) :
                new ObjectParameter("propertyId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetPropertyById_Result>("usp_GetPropertyById", propertyIdParameter);
        }
    
        public virtual int usp_InsertSingleProperty(string location, Nullable<int> bedroom, Nullable<int> bathroom, string confidentialNotes, Nullable<int> status, Nullable<bool> isDeleted, Nullable<System.DateTime> createdDate, Nullable<System.DateTime> updatedDate)
        {
            var locationParameter = location != null ?
                new ObjectParameter("location", location) :
                new ObjectParameter("location", typeof(string));
    
            var bedroomParameter = bedroom.HasValue ?
                new ObjectParameter("bedroom", bedroom) :
                new ObjectParameter("bedroom", typeof(int));
    
            var bathroomParameter = bathroom.HasValue ?
                new ObjectParameter("bathroom", bathroom) :
                new ObjectParameter("bathroom", typeof(int));
    
            var confidentialNotesParameter = confidentialNotes != null ?
                new ObjectParameter("confidentialNotes", confidentialNotes) :
                new ObjectParameter("confidentialNotes", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            var isDeletedParameter = isDeleted.HasValue ?
                new ObjectParameter("IsDeleted", isDeleted) :
                new ObjectParameter("IsDeleted", typeof(bool));
    
            var createdDateParameter = createdDate.HasValue ?
                new ObjectParameter("createdDate", createdDate) :
                new ObjectParameter("createdDate", typeof(System.DateTime));
    
            var updatedDateParameter = updatedDate.HasValue ?
                new ObjectParameter("updatedDate", updatedDate) :
                new ObjectParameter("updatedDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_InsertSingleProperty", locationParameter, bedroomParameter, bathroomParameter, confidentialNotesParameter, statusParameter, isDeletedParameter, createdDateParameter, updatedDateParameter);
        }
    
        public virtual int usp_UpdateProperty(Nullable<int> propertyID, string location, Nullable<int> bedroom, Nullable<int> bathroom, string confidentialNotes, Nullable<int> status, Nullable<bool> isDeleted, Nullable<System.DateTime> updatedDate)
        {
            var propertyIDParameter = propertyID.HasValue ?
                new ObjectParameter("propertyID", propertyID) :
                new ObjectParameter("propertyID", typeof(int));
    
            var locationParameter = location != null ?
                new ObjectParameter("location", location) :
                new ObjectParameter("location", typeof(string));
    
            var bedroomParameter = bedroom.HasValue ?
                new ObjectParameter("bedroom", bedroom) :
                new ObjectParameter("bedroom", typeof(int));
    
            var bathroomParameter = bathroom.HasValue ?
                new ObjectParameter("bathroom", bathroom) :
                new ObjectParameter("bathroom", typeof(int));
    
            var confidentialNotesParameter = confidentialNotes != null ?
                new ObjectParameter("confidentialNotes", confidentialNotes) :
                new ObjectParameter("confidentialNotes", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            var isDeletedParameter = isDeleted.HasValue ?
                new ObjectParameter("IsDeleted", isDeleted) :
                new ObjectParameter("IsDeleted", typeof(bool));
    
            var updatedDateParameter = updatedDate.HasValue ?
                new ObjectParameter("updatedDate", updatedDate) :
                new ObjectParameter("updatedDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_UpdateProperty", propertyIDParameter, locationParameter, bedroomParameter, bathroomParameter, confidentialNotesParameter, statusParameter, isDeletedParameter, updatedDateParameter);
        }
    
        public virtual ObjectResult<usp_GetAllProperties_Result> usp_GetAllProperties()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetAllProperties_Result>("usp_GetAllProperties");
        }
    
        public virtual ObjectResult<usp_GetAllRoles_Result> usp_GetAllRoles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetAllRoles_Result>("usp_GetAllRoles");
        }
    
        public virtual int usp_DeleteProperty(Nullable<int> propertyID)
        {
            var propertyIDParameter = propertyID.HasValue ?
                new ObjectParameter("propertyID", propertyID) :
                new ObjectParameter("propertyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_DeleteProperty", propertyIDParameter);
        }
    
        public virtual ObjectResult<usp_GetStaffByEmail_Result> usp_GetStaffByEmail(string email)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetStaffByEmail_Result>("usp_GetStaffByEmail", emailParameter);
        }
    }
}
