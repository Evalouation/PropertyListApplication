namespace PropertyList.BusinessLogic.Model
{
    public class PropertyDtoModel
    {
        public int PropertyID { get; set; }
        public string Location { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public string ConfidentialNotes { get; set; }
        public int Status { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
