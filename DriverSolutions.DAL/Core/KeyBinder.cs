using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.DAL
{
    public class KeyBinder
    {
        private List<KeyEntry> Binds;
        private List<KeyEntry> Revert;

        public KeyBinder()
        {
            this.Binds = new List<KeyEntry>();
            this.Revert = new List<KeyEntry>();
        }

        /// <summary>
        /// Clears binding keys and rollback keys
        /// </summary>
        public void Clear()
        {
            this.Binds.Clear();
            this.Revert.Clear();
        }

        /// <summary>
        /// Adds a new key for binding
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination objects</param>
        /// <param name="sourceProperty">Source object property</param>
        /// <param name="destinationProperty">Destination object property</param>
        public void AddKey(object source, object destination, string sourceProperty, string destinationProperty)
        {
            this.Binds.Add(new KeyEntry()
            {
                SourceObject = source,
                DestinationObject = destination,
                SourceProperty = source.GetType().GetProperty(sourceProperty),
                DestinationProperty = destination.GetType().GetProperty(destinationProperty)
            });
        }

        /// <summary>
        /// Adds a new key for binding
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">>Destination objects</param>
        /// <param name="property">Source and destination object property</param>
        public void AddKey(object source, object destination, string property)
        {
            this.Binds.Add(new KeyEntry()
            {
                SourceObject = source,
                DestinationObject = destination,
                SourceProperty = source.GetType().GetProperty(property),
                DestinationProperty = destination.GetType().GetProperty(property)
            });
        }

        /// <summary>
        /// Adds a new key to the rollback list
        /// </summary>
        /// <param name="originalValue">Original value to restore</param>
        /// <param name="model">Model to restore</param>
        /// <param name="propertyName">Property to restore</param>
        public void AddRollback(object originalValue, object model, string propertyName)
        {
            KeyEntry key = new KeyEntry();
            key.SourceObject = originalValue;
            key.DestinationObject = model;
            key.DestinationProperty = model.GetType().GetProperty(propertyName);
            this.Revert.Add(key);
        }

        /// <summary>
        /// Bind all keys to their objects
        /// </summary>
        public void BindKeys()
        {
            foreach (var bind in this.Binds)
            {
                var source = bind.SourceObject;
                var dest = bind.DestinationObject;

                var sprop = bind.SourceProperty;
                var dprop = bind.DestinationProperty;

                object value = sprop.GetValue(source);
                dprop.SetValue(dest,value);
            }

            this.Binds.Clear();
        }

        public void RollbackKeys()
        {
            foreach (var key in this.Revert)
            {
                var prop = key.DestinationProperty;
                prop.SetValue(key.DestinationObject, key.SourceObject, null);
            }

            this.Revert.Clear();
        }

        private class KeyEntry
        {
            public object SourceObject;
            public object DestinationObject;
            public PropertyInfo SourceProperty;
            public PropertyInfo DestinationProperty;
        }
    }
}
