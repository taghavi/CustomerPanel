
namespace Business
{
    /// <summary>
    /// BaseObject for any object that is nessessary to persist
    /// </summary>
    public class PersistentObject : IIdentityObject
    {
      
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        virtual public long? Id { get; set; }
    }
}