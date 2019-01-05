using CRMDevLabs.VirtualEntity.Extensibility.Base;
using CRMDevLabs.VirtualEntity.Extensibility.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRMDevLabs.VirtualEntity.Extensibility.Models
{
    public class People : ObjectModelBase, ITable, ITransformation
    {
        [Key]
        public virtual Guid Id { get; set; }

        public virtual int PersonID { get; set; }

        public virtual string FullName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual void TransformObject()
        {

        }
    }
}
