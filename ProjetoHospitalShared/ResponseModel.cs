namespace ProjetoHospitalShared
{
    using Flunt.Notifications;

    public class ResponseModel<T>
    {
        public ResponseModel()
        {
        }

        public ResponseModel(
            T data,
            IEnumerable<Notification> notifications = null)
        {
            this.Data = data;
            this.Notifications = notifications ?? Enumerable.Empty<Notification>();
        }

        public bool Success => this.Notifications is null || !this.Notifications.Any();

        public T Data { get; set; }

        public IEnumerable<Notification> Notifications { get; set; } = Enumerable.Empty<Notification>();
    }

    public class ResponseModel
    {
        public ResponseModel()
        {
        }

        public ResponseModel(
            IEnumerable<Notification> notifications)
        {
            this.Notifications = notifications ?? Enumerable.Empty<Notification>();
        }

        public IEnumerable<Notification> Notifications { get; set; } = Enumerable.Empty<Notification>();

        public bool Success => this.Notifications is null || !this.Notifications.Any();

        public bool Fail => this.Notifications?.Any() ?? false;
    }
}
