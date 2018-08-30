using PropertyList.Data.Model;

namespace PropertyList.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PropertyListing_DevEntities _db = new PropertyListing_DevEntities();

        public PropertyListing_DevEntities GetDB()
        {
            return _db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
