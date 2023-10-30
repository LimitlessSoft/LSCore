namespace LSCore.Contracts.Requests
{
    public class LSCoreSaveRequest
    {
        public int? Id { get; set; }

        public LSCoreSaveRequest()
        {

        }

        public LSCoreSaveRequest(int? Id)
        {
            this.Id = Id;
        }

        public bool IsNew { get => !Id.HasValue; }
        public bool IsOld { get => Id.HasValue; }
    }
}
