using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Exceptions
{
    [Serializable()]
    public class ModelNotFoundException : Exception
    {
        public Type ModelType { get; protected set; }

        public ModelNotFoundException()
            : base()
        {
        }

        public ModelNotFoundException(Type modelType)
            : base(modelType.Name + " did not found")
        {
            ModelType = modelType;
        }

        public ModelNotFoundException(string message, Type modelType)
            : base(message)
        {
            ModelType = modelType;
        }

        public ModelNotFoundException(string message)
            : base(message)
        {
        }
    }

    [Serializable()]
    public class ModelNotFoundException<TEntity> : ModelNotFoundException
        where TEntity : IEntity<Guid>
    {
        public ModelNotFoundException()
            : base()
        {
            ModelType = typeof(TEntity);
        }
    }
}
