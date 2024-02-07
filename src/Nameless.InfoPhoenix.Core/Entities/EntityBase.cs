namespace Nameless.InfoPhoenix.Entities {
    public abstract class EntityBase {
        #region Public Properties

        public Guid ID { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        #endregion
    }
}
