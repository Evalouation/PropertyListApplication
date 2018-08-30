using System;
using PropertyList.Data.Model;

namespace PropertyList.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        PropertyListing_DevEntities GetDB();
    }
}
