using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    /// <summary>
    /// <para>Determines the type of staff member </para>
    /// <para>The Staff Type is defined as the job function of the staff member</para>
    /// <inheritdoc cref="IEquatable{T}"/>
    /// </summary>
    public partial class StaffType : IEquatable<StaffType>
    {
        public StaffType()
        {
        }

        #region  Entity Properties
        /// <value>
        /// Primary Key in the database for the StaffTypes table
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <value>
        /// The Name/Title of the staff type
        /// </value>
        [Required]
        [StringLength(20)]
        public string Title { get; set; } = default!;

        /// <value>
        /// Description of the staff type
        /// </value>
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = default!;
        
        [InverseProperty(nameof(StaffTypesRoles.StaffTypeNavigation))]
        public virtual ICollection<StaffTypesRoles> StaffTypeRoleMaps { get; set; } = new HashSet<StaffTypesRoles>();

        /*
 * {"error":"
 * The [InverseProperty] attribute on property 'StaffRole.StaffTypeRoleMaps' is not valid.
 * The property 'StaffTypeNavigation' is not a valid navigation on the related type 'StaffTypesRoles'
 * . Ensure that the property exists and is a valid reference or collection navigation."}
 */
        #endregion

        #region Overrides
        public bool Equals(StaffType other)
        {
            return other is not null
                   && other.Id == Id
                   && !String.IsNullOrWhiteSpace(other.Title)
                   && other.Title.Equals(Title);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is StaffType other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 47;
        }

        public static bool operator ==(StaffType lhs, StaffType rhs)
        {
            return lhs?.Equals(rhs) ?? rhs is null;
        }

        public static bool operator !=(StaffType lhs, StaffType rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
